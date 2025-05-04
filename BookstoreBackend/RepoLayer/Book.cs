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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Book name is required")]
        [StringLength(2000, ErrorMessage = "Book name must be up to 2000 characters")]
        [Column("BookName",TypeName ="VARCHAR(100)")]
        public string BookName { get; set; }

        [Required(ErrorMessage ="Author's name is required")]
        [StringLength(100, ErrorMessage = "Author name must be up to 100 characters")]
        [Column(TypeName ="VARCHAR(100)")]
        public string Author { get; set; }

        [StringLength(20000, ErrorMessage = "Description can be up to 20000 characters")]
        [Column(TypeName ="TEXT")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Price { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Discount price cannot be negative")]
        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal DiscountPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string? BookImage { get; set; }

        [Column(TypeName = "INT")]
        public int AdminUserId { get; set; }

        [Column(TypeName = "DATETIME2")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "DATETIME2")]
        public DateTime? UpdatedAt { get; set; }
    }
}
