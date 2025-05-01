using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public interface IBookService
    {
        Task<List<Book>> GetBooksFromFileAsync();
    }
}
