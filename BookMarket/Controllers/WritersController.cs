using BookMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarket.Controllers
{
    public class WritersController:Controller
    {
        private readonly AppDbContext _context;
        public WritersController(AppDbContext context) 
        {
            _context  = context;
        }

        public IActionResult Index()
        {
            var isAdminClaim = User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
            bool isAdmin = false;
            if (isAdminClaim != null)
            {
                isAdmin = bool.Parse(isAdminClaim);
            }
            var IsRegistered = User.Claims.Any();
            ViewBag.IsRegistered = IsRegistered;
            // Pass the IsAdmin value to the view
            ViewBag.IsAdmin = isAdmin;
            var writers = _context.Writers.ToList();
            return View(writers);
        }
        public  IActionResult View(int Id)
        {
            var item = _context.Writers.First(x=>x.Id== Id);
            return View(item);
        }
        public IActionResult CreateRedirectorForm(Writer writer)
        {
            return View(writer);
        }
        public IActionResult CreateWriter(Writer writer)
        {
            if(ModelState.IsValid)
            {
                _context.Writers.Add(writer);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            return RedirectToAction("CreateRedirectorForm",writer);
        }
        public  IActionResult DeleteWriter(int id)
        {
            var writer = _context.Writers.FirstOrDefault(x=>x.Id== id);
            _context.Writers.Remove(writer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var writer = _context.Writers.FirstOrDefault(x=>x.Id== id);
            return View(writer);
        }
        [HttpPost]
        public IActionResult EditConfirm(Writer writer)
        {
            if (writer == null)
            {
                return NotFound();
            }
            _context.Writers.Update(writer); 
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
