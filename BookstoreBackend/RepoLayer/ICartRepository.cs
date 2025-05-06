using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface ICartRepository
    {
        Task<CartResponseDto> AddBookToCartAsync(int userId, int bookId);

        Task<List<Cart>> GetCartAsync(int userId);

        Task<Cart> UpdateCartAsync(int userId, int bookId, int quantity);

        Task<bool> DeleteCartAsync(int userId, int bookId);
    }
}
