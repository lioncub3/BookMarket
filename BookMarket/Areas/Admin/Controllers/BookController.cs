using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookMarket.Data;
using BookMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookMarket.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    public class BookController : Controller
    {
        private BooksContext db;
        private readonly IHostingEnvironment _hostingEnvironment;
        public BookController(BooksContext db, IHostingEnvironment _hostingEnvironment)
        {
            this.db = db;
            this._hostingEnvironment = _hostingEnvironment;
        }
        public IActionResult Index()
        {
            ViewData["Categorys"] = db.Categories;
            return View(db.Books);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);
            if (book.Photo != null)
            {
                string type = Path.GetExtension(book.Photo.FileName);
                string name = $"{DateTime.Now.ToString("ssmmhhddMMyyyy")}{type}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", name);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await book.Photo.CopyToAsync(fileStream);
                }

                book.PhotoUrl = "/photos/" + name;
            }

            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            Book book = db.Books.First(i => i.Id == Id);

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
                return View(model);
            }

            string fullPath = $"{_hostingEnvironment.WebRootPath}{model.PhotoUrl}";
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            if (model.Photo != null)
            {
                string type = Path.GetExtension(model.Photo.FileName);
                string name = $"{DateTime.Now.ToString("ssmmhhddMMyyyy")}{type}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", name);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }

                model.PhotoUrl = "/photos/" + name;
            }

            db.Books.Update(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Book book = db.Books.First(b => b.Id == Id);

            string fullPath = $"{_hostingEnvironment.WebRootPath}{book.PhotoUrl}";
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            if (book != null)
            {
                db.Remove(book);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}