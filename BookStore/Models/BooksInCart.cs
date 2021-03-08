using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    /// <summary>
    /// Class that represents books in chart
    /// </summary>
    public class BooksInCart
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
