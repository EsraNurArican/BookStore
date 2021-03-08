using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    /// <summary>
    /// Class represents the categories of the books
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Book> Books { get; set; }
    }
}
