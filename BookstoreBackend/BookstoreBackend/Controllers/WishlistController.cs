using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoLayer;
using ServiceLayer;

namespace BookstoreBackend.Controllers
{
    [Route("api/wishlist")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService wishlistService;
        private readonly IResponseHelper responseHelper;

        public WishlistController(IWishlistService wishlistService, IResponseHelper responseHelper)
        {
            this.wishlistService = wishlistService;
            this.responseHelper = responseHelper;
        }

        [HttpPost("addBook")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> AddBookToWishlist(int bookId)
        {
            var userIdClaim = User.FindFirst("Id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return null;
            }
            var (isDuplicate, wishlist) = await wishlistService.AddBookToWishlistAsync(userId, bookId);
            if (wishlist == null)
            {
                if (isDuplicate)
                {
                    return Conflict(responseHelper.Failure<string>("Cannot add to wishlist since this book is already present!"));
                }
                return NotFound(responseHelper.Failure<string>("Book not found!"));
            }
            return Ok(responseHelper.Success("Book added to wishlist successfully!", wishlist));
        }

        [HttpGet("getWishlist")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> GetWishlist()
        {
            var userIdClaim = User.FindFirst("Id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return null;
            }
            var wishlist = await wishlistService.GetWishlistAsync(userId);
            if (wishlist == null || wishlist.Count == 0)
            {
                return NotFound(responseHelper.Failure<string>("Wishlist is empty!"));
            }
            return Ok(responseHelper.Success("Wishlist retrieved successfully!", wishlist));
        }

        [HttpDelete("removeBook")]
        [Authorize(Roles ="USER")]
        public async Task<IActionResult> RemoveBookFromWishlist(int bookId)
        {
            var userIdClaim = User.FindFirst("Id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim==null||!int.TryParse(userIdClaim.Value, out int userId))
            {
                return null;
            }
            var deleted = await wishlistService.RemoveBookFromWishlistAsync(userId, bookId);
            if (!deleted)
            {
                return NotFound(responseHelper.Failure<string>("Book not found in wishlist!"));
            }
            return Ok(responseHelper.Success<string>("Book removed from wishlist successfully!"));
        }
    }
}