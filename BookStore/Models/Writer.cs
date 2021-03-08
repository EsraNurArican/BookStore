using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    /// <summary>
    /// Writer class to represent writer of the book
    /// </summary>
    public class Writer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Book> Books { get; set; } 
    }
}
