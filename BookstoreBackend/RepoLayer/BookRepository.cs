using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer
{
    public class BookRepository : IBookRepository
    {
        private readonly UserDbContext context;

        public BookRepository(UserDbContext context)
        {
            this.context = context;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var books = await context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            book.CreatedAt = DateTime.UtcNow;
            book.UpdatedAt = DateTime.UtcNow;
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book updatedBook)
        {
            var existing = await context.Books.FindAsync(id);
            if (existing == null)
            {
                return null;
            }
            existing.BookName = updatedBook.BookName;
            existing.Author = updatedBook.Author;
            existing.Description = updatedBook.Description;
            existing.BookImage = updatedBook.BookImage;
            existing.AdminUserId = updatedBook.AdminUserId;
            existing.UpdatedAt = DateTime.UtcNow;

            if (updatedBook.Quantity != existing.Quantity && existing.Quantity > 0)
            {
                decimal unitPrice = existing.Price / existing.Quantity;
                decimal unitDiscountPrice = existing.DiscountPrice / existing.Quantity;
                existing.Quantity = updatedBook.Quantity;
                existing.Price = unitPrice * updatedBook.Quantity;
                existing.DiscountPrice = unitDiscountPrice * updatedBook.Quantity;
            }
            else
            {
                existing.Quantity = updatedBook.Quantity;
                existing.Price = updatedBook.Price;
                existing.DiscountPrice = updatedBook.DiscountPrice;
            }
            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
