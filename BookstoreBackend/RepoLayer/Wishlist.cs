using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="INT")]
        public int UserId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
