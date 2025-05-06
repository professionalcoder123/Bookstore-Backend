using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer;
using ServiceLayer;

namespace BookstoreBackend.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;
        private readonly IResponseHelper responseHelper;

        public CartController(ICartService cartService, IResponseHelper responseHelper)
        {
            this.cartService = cartService;
            this.responseHelper = responseHelper;
        }

        [HttpPost("addBookToCart")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> AddBookToCart([FromBody] AddBookToCartDto cartDto)
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
            var cart = await cartService.AddBookToCartAsync(cartDto.BookId, User);
            if (cart == null)
            {
                return BadRequest(responseHelper.Failure<string>("Book not found!"));
            }
            return Ok(responseHelper.Success("Book added to cart successfully!", cart));
        }

        [HttpGet("getCart")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var cartData = await cartService.GetCartWithTotalAsync(User);
                if (cartData == null || cartData.CartItems==null || !cartData.CartItems.Any())
                {
                    return NotFound(responseHelper.Failure<List<Cart>>("Cart is empty"));
                }
                return Ok(responseHelper.Success("Cart retrieved successfully!", cartData));
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    responseHelper.Failure<List<Cart>>($"An error occurred : {e.Message}"));
            }
        }

        [HttpPut("updateCart")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartDto cartDto)
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
            var updatedCart = await cartService.UpdateCartAsync(User, cartDto);
            if (updatedCart == null)
            {
                return NotFound(responseHelper.Failure<string>("Cart item not found or removed due to zero quantity!"));
            }
            return Ok(responseHelper.Success("Cart item updated successfully!", updatedCart));
        }

        [HttpDelete("deleteCart/{bookId}")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> DeleteCart(int bookId)
        {
            var userIdClaim = User.FindFirst("Id");
            if(userIdClaim==null||!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(responseHelper.Failure<string>("Invalid token : User ID not found!")); ;
            }
            var deleted = await cartService.DeleteCartAsync(userId, bookId);
            if (!deleted)
            {
                return NotFound(responseHelper.Failure<string>("Cart item not found!"));
            }
            return Ok(responseHelper.Success<string>("Cart item deleted successfully!"));
        }
    }
}
