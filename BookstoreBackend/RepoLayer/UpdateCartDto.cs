using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class UpdateCartDto
    {
        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
