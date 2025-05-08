using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }

        public async Task<(bool isDuplicate, Wishlist? Wishlist)> AddBookToWishlistAsync(int userId, int bookId)
        {
            return await wishlistRepository.AddBookToWishlistAsync(userId, bookId);
        }

        public async Task<List<Wishlist>> GetWishlistAsync(int userId)
        {
            return await wishlistRepository.GetWishlistAsync(userId);
        }

        public async Task<bool> RemoveBookFromWishlistAsync(int userId, int bookId)
        {
            return await wishlistRepository.RemoveBookFromWishlistAsync(userId, bookId);
        }
    }
}
