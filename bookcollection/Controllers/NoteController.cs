using System.Net;
using bookcollection.Data;
using bookcollection.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookcollection.Controllers
{
    public class NoteController : Controller
    {
        private readonly ApplicationDBContext _db;

        public NoteController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? id)
        {
            if(id != null || id > 0)
            {
                var obj = _db.Books.Find(id);
                var viewModel = new NoteViewModel
                {
                    ThisBook = obj,
                    Notes = _db.Notes.Where(n => n.BookID == id).ToList()
                };

                
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoteViewModel obj)
        {

            try
            {
                if (obj != null)
                {
                    var newNote = obj.NewNote;

                    if (newNote.ImageFile != null && newNote.ImageFile.Length > 0)
                    {
                        newNote.Image64 = ConvertImageToBase64(newNote.ImageFile);
                    }
                    else
                    {
                        newNote.Image64 = null;
                    }

                    _db.Notes.Add(newNote);
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

        public IActionResult Delete(int? id) {

            if (id is null || id is 0)
            {
                return NotFound();
            }

            var obj = _db.Notes.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Notes.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
