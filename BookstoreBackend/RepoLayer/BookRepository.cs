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

        public async Task<List<Book>> GetBooksAsync()
        {
            var books = await context.Books.ToListAsync();
            return books;
        }
    }
}
