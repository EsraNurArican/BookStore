using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admin,Editor")] //sadece admin veya editor product controller'a girebilir
    public class BooksController : Controller
    {
        private IBookService bookService;
        private ICategoryService categoryService;
        private IPublisherService publisherService;
        private IWriterService writerService;
        

        public BooksController(IBookService bookService, ICategoryService categoryService,IPublisherService publisherService,IWriterService writerService)
        {
            this.bookService = bookService;
            this.categoryService = categoryService;
            this.writerService = writerService;
            this.publisherService = publisherService;
        }

        public IActionResult Index()
        {
            var products = bookService.GetBooks();
            return View(products);
        }

        public IActionResult Create()
        {
            //yeni ürün eklerken kategori id girmesi mantıksız
            //asp-select ile kategorileri viewbagle gönderelim ve onu göstersin
           
            List<SelectListItem> selectListItemsCategory = getCategoriesForSelect();
            List<SelectListItem> selectListItemsWriters = getWritersForSelect();
            List<SelectListItem> selectListItemsPublishers = getPublisherForSelect();

            ViewBag.Items = selectListItemsCategory;
            ViewBag.ItemsWriter = selectListItemsWriters;
            ViewBag.Publisher = selectListItemsPublishers;

            return View();
        }


        [HttpPost]
        public IActionResult Create(Book book)
        {
            List<SelectListItem> selectListItemsCategory = getCategoriesForSelect();
            List<SelectListItem> selectListItemsWriters = getWritersForSelect();
            List<SelectListItem> selectListItemsPublishers = getPublisherForSelect();

            ViewBag.Items = selectListItemsCategory;
            ViewBag.ItemsWriter = selectListItemsWriters;
            ViewBag.Publisher = selectListItemsPublishers;

            // if (ModelState.IsValid)
            //{
            bookService.AddBook(book);
                return RedirectToAction(nameof(Index)); //nameof yazım hatasına karşı önlem olarak kullanıldı
           // }
            /*else
            {
                ValidationProblem();
                //return View();
            }*/


            return View();
        }

        public IActionResult Edit(int id)
        {
            var existingBook = bookService.GetBooksById(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            ViewBag.Items = getCategoriesForSelect(); //ürünün kategorisini değiştirmek isterse,selectListItem ile kategorileri viewBag'e yolla
            ViewBag.ItemsWriter = getWritersForSelect();
            ViewBag.Publisher = getPublisherForSelect();
            return View(existingBook);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            ViewBag.Items = getCategoriesForSelect();
            ViewBag.ItemsWriter = getWritersForSelect();
            ViewBag.Publisher = getPublisherForSelect();
            if (true)
            {
                int affectedRowCount = bookService.EditBook(book);
                string message = affectedRowCount > 0 ? $"{book.Name} isimli ürün güncellendi." : "Bir problem oluştu";

                return RedirectToAction(nameof(Index)); //ajax bildirimi ekranda basılmadığı icin bu sekilde değiştirildi.
                //return Json(message); // json return ettik,cünkü ajax ile yapacagız.
            }
            //bir hata olmussa yine aynı viewi açacak, yine categorileri göndermemiz lazım
           
            return View();
           
        }

        public IActionResult Delete(int id)
        {
            var deletingProduct = bookService.GetBooksById(id);
            return View(deletingProduct);
           
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteOk(int id)
        {
            var deletingProduct = bookService.GetBooksById(id);
            bookService.DeleteBook(deletingProduct);
            return RedirectToAction(nameof(Index)); //sildikten sonra orada kalmasın,ürünlere geri dönsün
            
        }

        public IActionResult Details(int id)
        {
            var book = bookService.GetBooksById(id);
            return View(book);
        }
        private List<SelectListItem> getPublisherForSelect()
        {
            var publishers = publisherService.GetPublishers();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            publishers.ToList().ForEach(publisher => selectListItems.Add(new SelectListItem
            {
                Text = publisher.Name,
                Value = publisher.Id.ToString()

            }));
            return selectListItems;
        }

        private List<SelectListItem> getWritersForSelect()
        {
            var writers = writerService.GetWriters();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            writers.ToList().ForEach(writers => selectListItems.Add(new SelectListItem
            {
                Text=writers.Name,
                Value=writers.Id.ToString()
            }));
            return selectListItems;
        }

        private List<SelectListItem> getCategoriesForSelect()
        {
            var categories = categoryService.GetCategories();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            categories.ToList().ForEach(category => selectListItems.Add(new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            }));
            return selectListItems;
        }
    }

}
