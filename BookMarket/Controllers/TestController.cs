using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using BookMarket.Models;
using BookMarket.Filters;
using Microsoft.AspNetCore.Http.HttpResults;


namespace BookMarket.Controllers
{
    public class TestController : Controller
    {
       // [ExceptionHandel]
        public IActionResult Index(string Sname)
        {
            throw new Exception("Error");
            ViewBag.ImgUploaded = false;
            ViewData["ImageUrl"] = Sname;
            return View();
        }
        //
      
        /*if (file != null && file.Length > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine("images/", filename);
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path); // Get full path
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                ViewBag.ImgUploaded = true;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ImgUploaded = false;
                return RedirectToAction("Index");
            }*/

    }
}
