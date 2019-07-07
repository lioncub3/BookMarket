using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookMarket.Models;
using BookMarket.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BookMarket.Controllers
{
    public class HomeController : Controller
    {
        private BooksContext db;

        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public HomeController(BooksContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Index(int Id = 0)
        {
            ViewBag.Categories = db.Categories;
            if(Id > 0)
                return View(db.Books.Where(b => b.CategoryId == Id));
            else
                return View(db.Books);
        }

        public IActionResult Info(int Id)
        {
            ViewBag.Comments = db.Comments.Where(c => c.BookId == Id);
            return View(db.Books.First(b => b.Id == Id));
        }

        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            if (!ModelState.IsValid)
                return View(model);

            IdentityUser user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result
                    = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                    return Redirect(ReturnUrl ?? "/");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.Name };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
