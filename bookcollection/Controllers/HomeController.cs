using System.Diagnostics;
using bookcollection.Data;
using bookcollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            try
            {
                var viewModel = new BookViewModel
                {
                    Books = _db.Books.ToList()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return RedirectToAction("Error", new { errorMessage = ex.Message });
            }

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


        public IActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage; // ???????????????????????? View
            return View();
        }
    }

}
