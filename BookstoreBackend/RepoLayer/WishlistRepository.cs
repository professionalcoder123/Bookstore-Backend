using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly UserDbContext context;

        public WishlistRepository(UserDbContext context)
        {
            this.context = context;
        }

        public async Task<(bool isDuplicate, Wishlist? Wishlist)> AddBookToWishlistAsync(int userId, int bookId)
        {
            var book = await context.Books.FindAsync(bookId);
            if (book == null)
            {
                return (false, null);
            }
            bool exists = await context.Wishlist.AnyAsync(w => w.UserId == userId && w.BookId == bookId);
            if (exists)
            {
                return (true, null);
            }
            var wishlist = new Wishlist
            {
                UserId = userId,
                BookId = bookId,
                Book = book
            };
            await context.Wishlist.AddAsync(wishlist);
            await context.SaveChangesAsync();
            return (false, wishlist);
        }

        public async Task<List<Wishlist>> GetWishlistAsync(int userId)
        {
            return await context.Wishlist
                .Include(w => w.Book)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> RemoveBookFromWishlistAsync(int userId, int bookId)
        {
            var wishlistItem = await context.Wishlist
                .FirstOrDefaultAsync(w => w.UserId == userId && w.BookId == bookId);
            if (wishlistItem == null)
            {
                return false;
            }
            context.Wishlist.Remove(wishlistItem);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
