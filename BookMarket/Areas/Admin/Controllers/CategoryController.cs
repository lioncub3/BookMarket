using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMarket.Data;
using BookMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMarket.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private BooksContext db;
        public CategoryController(BooksContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            ViewBag.Categories = db.Categories;
            return View();
        }

        [HttpPost]
        public IActionResult Index(Category category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = db.Categories;
                return View(category);
            }

            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Category category = db.Categories.First(c => c.Id == Id);

            if(category != null)
            {
                db.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}