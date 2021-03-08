using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore.Controllers
{
    /// <summary>
    /// Controller class for cart actions
    /// </summary>
    public class CartController : Controller
    {
        private IBookService bookService;

        /// <summary>
        /// Constructor that takes bookService generated from IBookService interface
        /// </summary>
        /// <param name="bookService"></param>
        public CartController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult Index(string returnUrl)
        {
            var cart = GetCart();          //önce sepetten ürünleri çek
            ViewBag.ReturnUrl = returnUrl; // return urli view bag'e aktardık
            return View(cart);             // aldıgın cart'ı view'e gönder
        }

        [HttpPost]
        public IActionResult AddToCart(int id, string returnUrl)
        {
            var selectedProduct = bookService.GetBooksByIdNoInclude(id);
            if (selectedProduct == null)
            {
                return NotFound(); //böyle bir item yok
            }
            Cart cart = GetCart() ?? new Cart();

            cart.AddItem(selectedProduct, 1);

            SaveCart(cart);      //sessiona cartı kaydet 
            return RedirectToAction(nameof(Index), nameof(Cart), new { returnUrl = returnUrl });   // actiona yönlendir
        }

        public IActionResult CompleteShop()
        {
            return View();
        }

        // Daha önce eklenen session'u okur
        // Session içerisinde böyle bir eleman var mı bunu kontrol eder.
        Cart GetCart()
        {
            var data = HttpContext.Session.GetString("sepet");
            if (data == null)
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<Cart>(data);
            }
            
        }


        /// <summary>
        /// Sessionu kaydeden metod
        /// </summary>
        /// <param name="cart"> kaydedeceği sepeti parametre olarak alır</param>
        void SaveCart(Cart cart)
        {
            // cart nesnesini stringe dönüstürmek zorundayız(serialization)
            HttpContext.Session.SetString("sepet", JsonConvert.SerializeObject(cart));//sepetim session adımız
        }
    }
}

