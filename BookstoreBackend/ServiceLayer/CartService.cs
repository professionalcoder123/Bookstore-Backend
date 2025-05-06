using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<CartResponseDto> AddBookToCartAsync(int bookId, ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("Id") ?? user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim==null||!int.TryParse(userIdClaim.Value,out int userId))
            {
                return null;
            }
            return await cartRepository.AddBookToCartAsync(userId, bookId);
        }

        public async Task<CartResponseDto> GetCartWithTotalAsync(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("Id") ?? user.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim==null||!int.TryParse(userIdClaim.Value, out int userId))
            {
                return null;
            }
            var cartItems = await cartRepository.GetCartAsync(userId);
            decimal totalCartPrice = cartItems.Sum(item => item.TotalPrice);

            return new CartResponseDto
            {
                CartItems = cartItems,
                TotalCartPrice = totalCartPrice
            };
        }

        public async Task<Cart> UpdateCartAsync(ClaimsPrincipal user, UpdateCartDto cartDto)
        {
            var userIdClaim = user.FindFirst("Id") ?? user.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim==null||!int.TryParse(userIdClaim.Value, out int userId))
            {
                return null;
            }
            return await cartRepository.UpdateCartAsync(userId, cartDto.BookId, cartDto.Quantity);
        }

        public async Task<bool> DeleteCartAsync(int userId, int bookId)
        {
            return await cartRepository.DeleteCartAsync(userId, bookId);
        }
    }
}
