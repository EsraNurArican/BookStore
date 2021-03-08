using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private BookStoreDbContext dbContext;

        public BookService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddBook(Book book)
        {
            dbContext.Books.Add( book);
            dbContext.SaveChanges(); //add db
            
        }

        public void DeleteBook(Book deletingBook)
        {
            dbContext.Books.Remove(deletingBook);
            dbContext.SaveChanges();
        }

        public int EditBook(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public List<Book> GetBooks()
        {
            var books = dbContext.Books.AsNoTracking().ToList();
            return books;
        }

        public List<Book> GetBooksByCategoryId(int categoryId)
        {
            return dbContext.Books.AsNoTracking().Where(b => b.CategoryId == categoryId).ToList();
        }

        public Book GetBooksById(int id)
        {
            //detay sayfasında category id yerine categori adını verebilmek icin buradan category yollamamız lazım
            return dbContext.Books.AsNoTracking().Include(x=>x.Category).FirstOrDefault(x => x.Id == id);
            //ınclude category kaldırınca sepete ekleden gelen exception sorunu çözüldü
            //.Include(x => x.Category)

        }
        public Book GetBooksByIdNoInclude(int id)
        {
            return dbContext.Books.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
    }
}
