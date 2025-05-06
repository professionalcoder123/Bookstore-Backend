using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class AddBookToCartDto
    {
        [Required(ErrorMessage ="Book ID is required")]
        public int BookId { get; set; }
    }
}
