using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class CartResponseDto
    {
        public List<Cart> CartItems { get; set; }
        public decimal TotalCartPrice { get; set; }
    }
}
