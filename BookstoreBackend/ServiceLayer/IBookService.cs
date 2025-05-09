﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public interface IBookService
    {
        Task<List<Book>> GetBooksAsync();

        Task<Book> GetBookByIdAsync(int id);

        Task<Book> AddBookAsync(Book book);

        Task<Book> UpdateBookAsync(int id, Book updatedBook);

        Task<bool> DeleteBookAsync(int id);
    }
}
