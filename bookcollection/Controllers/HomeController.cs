using System.Diagnostics;
using bookcollection.Data;
using bookcollection.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookcollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;
        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Book> allBook = _db.Books;
            return View(allBook);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);
        }
    }
}
