using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RepoLayer;

namespace ServiceLayer
{
    public class BookService : IBookService
    {
        private readonly string filePath;

        public BookService(IConfiguration configuration)
        {
            filePath = configuration["BookFilePath"];
        }

        public async Task<List<Book>> GetBooksFromFileAsync()
        {
            if (!File.Exists(filePath))
            {
                return new List<Book>();
            }
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var books = await JsonSerializer.DeserializeAsync<List<Book>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return books ?? new List<Book>();
        }
    }
}
