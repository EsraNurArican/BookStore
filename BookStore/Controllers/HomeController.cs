using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBookService bookService;

        public HomeController(ILogger<HomeController> logger,IBookService bookService)
        {
            _logger = logger;
            this.bookService = bookService;
        }


        public IActionResult Index(int page=1, int catid=0)
        {
            var books = catid == 0 ? bookService.GetBooks() : bookService.GetBooksByCategoryId(catid);
            var pageSize = 4;


            // dinamik sayfalama
            var pagingBooks = books.OrderBy(p => p.Id)
                                         .Skip((page - 1) * pageSize) //atlanacak satır-eleman sayısı
                                         .Take(pageSize);         //atladıktan sonra alınacak eleman sayısı
            /*
             * 1. sayfayı görmek için 0 atla(hiç atlamadan) page size kadar göster
             * 2. sayfa 4 atla 4 göster (gösterilen eleman pageSize)
             * 3. sayfada 8 atla 4 göster
             * . . .
             */

            //sayfalama işleminde bir sayfada kaç ürün göstereceğimizi belirledik
            //dinamik yaklaşım
            var totalBook = books.Count;
            var totalPage = Math.Ceiling((decimal)totalBook / pageSize); //celing() yukarı yuvarla
            ViewBag.TotalPages = totalPage;
            ViewBag.CatId = catid;


            //return View(products) eski hali, paging işlemi yaotıktan sonra PagingProducts'u return ettik
            return View(pagingBooks);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
