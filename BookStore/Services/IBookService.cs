using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();
        List<Book> GetBooksByCategoryId(int categoryId);

        Book GetBooksByIdNoInclude(int id);
        void AddBook(Book book);
        Book GetBooksById(int id);
        int EditBook(Book book);
        void DeleteBook(Book deletingBook);
    }
}
