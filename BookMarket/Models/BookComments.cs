using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMarket.Models
{
    public class BookComments
    {
        public Book Book { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
