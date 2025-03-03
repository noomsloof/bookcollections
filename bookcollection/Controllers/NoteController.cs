using bookcollection.Data;
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
                return View(obj);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

    }
}
