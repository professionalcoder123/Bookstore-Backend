using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public interface IWishlistService
    {
        Task<(bool isDuplicate, Wishlist? Wishlist)> AddBookToWishlistAsync(int userId, int bookId);

        Task<List<Wishlist>> GetWishlistAsync(int userId);

        Task<bool> RemoveBookFromWishlistAsync(int userId, int bookId);
    }
}
