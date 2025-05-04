using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using RepoLayer;

namespace ServiceLayer
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await bookRepository.GetBookByIdAsync(id);
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await bookRepository.GetBooksAsync();
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            return await bookRepository.AddBookAsync(book);
        }

        public async Task<Book> UpdateBookAsync(int id, Book updatedBook)
        {
            return await bookRepository.UpdateBookAsync(id, updatedBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await bookRepository.DeleteBookAsync(id);
        }
    }
}
