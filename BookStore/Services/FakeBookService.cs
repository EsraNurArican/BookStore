using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class FakeBookService : IBookService
    {
        public List<Book> books = new List<Book>
        {
             new Book{Id=1 ,Name="book1", ImageUrl="https://cdn.dsmcdn.com/mnresize/415/622/ty8/Book/media/images/20200729/16/6325872/16639396/1/1_org.jpg",
                    Description="Kadın Kol Saati",
                    Writer=new Writer{ Name="esra"},
                    Category=new Category{Name="roman" },Publisher=new Publisher{Name="tahlil" },
                    Price=139.90M,Discount=0.55,Rating=4},

                new Book{Id=2 ,Name="book2", ImageUrl="https://cdn.dsmcdn.com/mnresize/415/622/ty8/Book/media/images/20200729/16/6325872/16639396/1/1_org.jpg",
                    Description="Kadın Kol Saati",
                    Writer=new Writer{ Name="esra"},
                    Category=new Category{Name="roman" },Publisher=new Publisher{Name="tahlil" },
                    Price=139.90M,Discount=0.55,Rating=4},

        };
         public List<Book> GetBooks()
         {
            return books;
         }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void DeleteBook(Book deletingBook)
        {
            books.Remove(deletingBook);
        }

        public int EditBook(Book book)
        {
            throw new NotImplementedException();
            // books.Entry(book).State = EntityState.Modified;
        }

       

        public List<Book> GetBooksByCategoryId(int categoryId)
        {
            return books.Where(b => b.CategoryId == categoryId).ToList();
            
        }

        public Book GetBooksById(int id)
        {
            return books.FirstOrDefault(x => x.Id == id);
           
        }

        public Book GetBooksByIdNoInclude(int id)
        {
            return books.FirstOrDefault(x => x.Id == id);
        }
    }
}
