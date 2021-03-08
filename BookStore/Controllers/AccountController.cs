using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
       
        private IUserService userService;

        public IActionResult Index()
        {
            return View();
        }

        public AccountController(IUserService userService)
        {
            this.userService = userService;

        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost] //sayfadan veriyi alacak
        //kullanıcı adı şifre doğru mu değil mi buna bakması lazım
        //user login olmaya calıstıgında bu metod calısacak
        public async Task<IActionResult> Login(UserLoginModel userLoginModel, string returnUrl)
        {
            var user = userService.ValidUser(userLoginModel.UserName, userLoginModel.Password);
            if (user != null) //eger user boş değilse,yetki verecegiz
            {
                List<Claim> claims = new List<Claim>(); //claimde sifre tutulmaz!
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                //claims.Add(new Claim(ClaimTypes.Role, "Admin")); //bu sekilde yazarsak siteye giren herkes admin olarak girer
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name)); //Role classı ekledikten sonra bu şekilde değişti.

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal); //user icin oturum actı
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl); //local url ise oraya yönlendir
                }
                return Redirect("/"); // anasayfaya yönlendir
            }

            ModelState.AddModelError("hata", "Kullanıcı adı ya da şifre yanlış");
            return View();

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // cıkıs yap, bu cookie'yi gecersiz kıl
            return Redirect("/");   // basit yönlendirme yöntemi

        }
    }
}
