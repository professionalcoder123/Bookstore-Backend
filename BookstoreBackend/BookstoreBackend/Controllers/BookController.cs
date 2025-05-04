using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer;
using ServiceLayer;

namespace BookstoreBackend.Controllers
{
    [Authorize]
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IResponseHelper responseHelper;

        public BookController(IBookService bookService, IResponseHelper responseHelper)
        {
            this.bookService = bookService;
            this.responseHelper = responseHelper;
        }

        [HttpGet("getBooks")]
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await bookService.GetBooksAsync();
                if (books == null || !books.Any())
                {
                    return NotFound(responseHelper.Failure<List<Book>>("No books found!"));
                }
                return Ok(responseHelper.Success("Book data retrieved successfully!", books));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, responseHelper.Failure<List<Book>>($"An error occurred : {e.Message}"));
            }
        }

        [HttpGet("getBook/{id}")]
        [Authorize(Roles ="ADMIN, USER")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(responseHelper.Failure<string>("Book not found!"));
            }
            return Ok(responseHelper.Success("Book retrieved successfully!", book));
        }

        [HttpPost("addBook")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors.Select(err => new ValidationError
                    {
                        Field = e.Key,
                        Message = err.ErrorMessage
                    })).ToList();

                return BadRequest(responseHelper.Failure("Validation failed!", errors));
            }
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (idClaim != null && int.TryParse(idClaim.Value, out int adminUserId))
            {
                book.AdminUserId = adminUserId;
            }
            book.BookImage = string.IsNullOrWhiteSpace(book.BookImage) ? null : book.BookImage;
            book.CreatedAt = DateTime.UtcNow;
            book.UpdatedAt = DateTime.UtcNow;
            var addedBook = await bookService.AddBookAsync(book);
            return Ok(responseHelper.Success("Book added successfully!", addedBook));
        }

        [HttpPut("updateBook/{id}")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors.Select(err => new ValidationError
                    {
                        Field = e.Key,
                        Message = err.ErrorMessage
                    })).ToList();

                return BadRequest(responseHelper.Failure("Validation failed!", errors));
            }
            book.BookImage = string.IsNullOrWhiteSpace(book.BookImage) ? null : book.BookImage;
            var adminIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (adminIdClaim == null)
            {
                return Unauthorized(responseHelper.Failure<string>("Invalid token: Admin ID not found."));
            }

            book.AdminUserId = int.Parse(adminIdClaim.Value);
            book.BookImage = string.IsNullOrWhiteSpace(book.BookImage) ? null : book.BookImage;
            book.UpdatedAt = DateTime.UtcNow;
            var result = await bookService.UpdateBookAsync(id, book);
            if (result == null)
            {
                return NotFound(responseHelper.Failure<string>("Book not found!"));
            }
            return Ok(responseHelper.Success("Book updated successfully!", result));
        }

        [HttpDelete("deleteBook/{id}")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await bookService.DeleteBookAsync(id);
            if (!deleted)
            {
                return NotFound(responseHelper.Failure<string>("Book not found!"));
            }
            return Ok(responseHelper.Success<string>("Book deleted successfully!"));
        }
    }
}