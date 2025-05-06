using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="INT")]
        public int UserId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required(ErrorMessage ="Book price is required")]
        [Column(TypeName ="DECIMAL(10,2)")]
        public decimal BookPrice { get; set; }

        [Required(ErrorMessage ="Book quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage ="Quantity cannot be negative")]
        [Column(TypeName ="INT")]
        public int BookQuantity { get; set; }

        [Column(TypeName ="DECIMAL(10,2)")]
        public decimal TotalPrice { get; set; }

        public virtual Book Book { get; set; }
    }
}
