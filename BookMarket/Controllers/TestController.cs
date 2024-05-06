using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;


namespace BookMarket.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index(string Sname)
        {
            ViewBag.ImgUploaded = false;
            ViewData["ImageUrl"] = Sname;
            return View();
        }
        //
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            string name = "";
            if(file !=null && file.Length > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine("images/", filename);
                var FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);
                ViewData["ImageUrl"] = filename;
                name = filename;
                using (var stream = new FileStream(FullPath, FileMode.Create))
                {
                     file.CopyTo(stream);
                }
                ViewBag.ImgUploaded = true;
            }
            else
            {
                ViewBag.ImgUploaded = false;
            }
            return RedirectToAction("Index",new {Sname = name});
        }
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
