using System;
using System.Collections.Generic;
using System.Formats.Asn1;
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

        public async Task<List<Book>> GetBooksAsync()
        {
            return await bookRepository.GetBooksAsync();
        }
    }
}
