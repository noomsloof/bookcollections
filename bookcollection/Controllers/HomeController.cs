using System.Diagnostics;
using bookcollection.Data;
using bookcollection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bookcollection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;
        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchQuery)
        {
            if (searchQuery != null)
            {
                try
                {
                    var viewModel = new BookViewModel
                    {
                        Books = _db.Books.Where(b => b.BookName.Contains(searchQuery) || b.Description.Contains(searchQuery) || b.Author.Contains(searchQuery) || b.Category.Contains(searchQuery)).ToList()
                    };
                    return View(viewModel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return RedirectToAction("Error", new { errorMessage = ex.Message });
                }
            }
            else
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
            
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookViewModel obj)
        {
            try { 
            if (obj!=null)
            {
                    var newBook = obj.NewBook;
                    if(newBook.ImageFile != null && newBook.ImageFile.Length > 0)
                    {
                        newBook.Image64 = ConvertImageToBase64(newBook.ImageFile);
                    }

                    newBook.Status = 0;
                    newBook.Score = 0;

                _db.Books.Add(obj.NewBook);
                _db.SaveChanges();
                return RedirectToAction("Index");

                }
                else
                {
                    return RedirectToAction("Error", new { errorMessage = "Error no data." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return RedirectToAction("Error", new { errorMessage = ex.Message });
            }
        }

        public string ConvertImageToBase64(IFormFile imageFile)
        {
            using (var ms = new MemoryStream())
            {
                imageFile.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public IActionResult Delete(int? id)
        {
            if(id is null || id is 0)
            {
                return NotFound();
            }

            var obj = _db.Books.Find(id);

            if(obj == null)
            {
                return NotFound();
            }

            var objNote = _db.Notes.Where(n => n.BookID == id).ToList();
            _db.Notes.RemoveRange(objNote);

            _db.Books.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.Books.Find(Id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book obj)
        {
            if(obj == null)
            {
                return NotFound();
            }

            var inDB = _db.Books.AsNoTracking().FirstOrDefault(b => b.Id == obj.Id);

            if (inDB == null)
            {
                return NotFound();
            }

            if (obj.ImageFile != null && obj.ImageFile.Length > 0)
            {
                obj.Image64 = ConvertImageToBase64(obj.ImageFile);
            }
            else
            {
                obj.Image64 = inDB.Image64;
            }

            if(inDB.Status.HasValue && inDB.Score.HasValue)
            {
                obj.Status = inDB.Status;
                obj.Score = inDB.Score;
            }
            else
            {
                obj.Status = 0;
                obj.Score = 0;
            }


            _db.Books.Update(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult StatusUpdate(int Id, int Status)
        {
            var book = _db.Books.Find(Id);
            if (book != null)
            {
                book.Status = Status;
                _db.Entry(book).Property(b => b.Status).IsModified = true; // บอกให้ EF อัปเดตแค่ Status
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public IActionResult ScoreUpdate(int Id, int Score)
        {
            var book = _db.Books.Find(Id);
            if (book != null)
            {
                book.Score = Score;
                _db.Entry(book).Property(b => b.Score).IsModified = true; // บอกให้ EF อัปเดตแค่ Score
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return NotFound();
            }

        }

    }

}
