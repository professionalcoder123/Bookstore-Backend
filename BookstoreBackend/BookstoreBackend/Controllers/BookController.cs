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
    }
}