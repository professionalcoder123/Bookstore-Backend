using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer
{
    public class CartRepository : ICartRepository
    {
        private readonly UserDbContext context;

        public CartRepository(UserDbContext context)
        {
            this.context = context;
        }

        public async Task<CartResponseDto> AddBookToCartAsync(int userId, int bookId)
        {
            var book = await context.Books.FindAsync(bookId);
            if (book == null)
            {
                return null;
            }
            var cartItem = await context.Cart
                .Include(c => c.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == bookId);
            if (cartItem != null)
            {
                cartItem.BookQuantity += 1;
                cartItem.TotalPrice = cartItem.BookQuantity * book.Price;
                cartItem.BookPrice = book.Price;
                context.Cart.Update(cartItem);
            }
            else
            {
                cartItem = new Cart
                {
                    UserId = userId,
                    BookId = bookId,
                    BookQuantity = 1,
                    TotalPrice = book.Price,
                    BookPrice = book.Price
                };
                await context.Cart.AddAsync(cartItem);
            }
            await context.SaveChangesAsync();
            var updatedCart = await context.Cart
                .Where(c => c.UserId == userId)
                .ToListAsync();

            decimal totalCartPrice = updatedCart.Sum(c => c.TotalPrice);
            return new CartResponseDto
            {
                CartItems = updatedCart,
                TotalCartPrice = totalCartPrice
            };
        }

        public Task<List<Cart>> GetCartAsync(int userId)
        {
            return context.Cart.Where(c => c.UserId == userId)
                .Include(c => c.Book).ToListAsync();
        }

        public async Task<Cart> UpdateCartAsync(int userId, int bookId, int quantity)
        {
            var cartItem = await context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == bookId);
            if (cartItem == null)
            {
                return null;
            }
            if (quantity <= 0)
            {
                context.Cart.Remove(cartItem);
                await context.SaveChangesAsync();
                return null;
            }
            cartItem.BookQuantity = quantity;
            cartItem.TotalPrice = quantity * cartItem.BookPrice;
            await context.SaveChangesAsync();
            cartItem = await context.Cart
                .Include(c => c.Book)
                .FirstOrDefaultAsync(c => c.Id == cartItem.Id);
            return cartItem;
        }

        public async Task<bool> DeleteCartAsync(int userId, int bookId)
        {
            var cartItem = await context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == bookId);
            if (cartItem == null)
            {
                return false;
            }
            context.Cart.Remove(cartItem);
            await context.SaveChangesAsync();
            return true;
        }
    }
}