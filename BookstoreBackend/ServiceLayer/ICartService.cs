using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public interface ICartService
    {
        Task<CartResponseDto> AddBookToCartAsync(int bookId, ClaimsPrincipal user);

        Task<CartResponseDto> GetCartWithTotalAsync(ClaimsPrincipal user);

        Task<Cart> UpdateCartAsync(ClaimsPrincipal user, UpdateCartDto cartDto);

        Task<bool> DeleteCartAsync(int userId, int bookId);
    }
}
