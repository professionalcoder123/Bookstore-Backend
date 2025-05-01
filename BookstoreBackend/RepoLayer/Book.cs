using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Book name is required")]
        [Column("BookName",TypeName ="VARCHAR(100)")]
        public string BookName { get; set; }

        [Required(ErrorMessage ="Author's name is required")]
        [Column(TypeName ="VARCHAR(100)")]
        public string Author { get; set; }

        [Column(TypeName ="TEXT")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal DiscountPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string? BookImage { get; set; }
    }
}
