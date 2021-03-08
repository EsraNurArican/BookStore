using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    /// <summary>
    /// Cart class
    /// </summary>
    public class Cart
    {
        //user's cart
        private List<BooksInCart> books = new List<BooksInCart>();

        
        /// <summary>
        /// Add item to cart
        /// If book doesn't exist in the collection,add
        /// Else if book exists in the collection,increase its quantitiy
        /// </summary>
        /// <param name="book"></param>
        /// <param name="quantity"></param>
        public void AddItem(Book book, int quantity)
        {
            var existingProduct = books.FirstOrDefault(x => x.Book.Id == book.Id);
            if (existingProduct == null) //if book doesnt exist in cart
            {
                books.Add(new BooksInCart { Book = book, Quantity = quantity });
            }
            else //if book already exist in cart
            {
                existingProduct.Quantity += quantity;
            }
        }

        
        /// <summary>
        /// Remove book from cart
        /// </summary>
        /// <param name="product">given book object to be removed</param>
        public void Remove(Book book) => books.RemoveAll(x => x.Book.Id == book.Id);
        
        /// <summary>
        /// Clear cart 
        /// </summary>
        public void Clear() => books.Clear();
        
        /// <summary>
        /// Calculate total value of the books in the cart
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalValue() => books.Sum(x => x.Book.Price * x.Quantity);

        /// <summary>
        /// Written to be able to reach books in the cart
        /// </summary>
        public IEnumerable<BooksInCart> Books => books;
    }
}
