using BookMarket.Models;
using BookMarket.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.IO;
using System.Net;

namespace BookMarket.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult LoginP(Account account)
        {
            if (ModelState.IsValid)
            {
                return View(account);
            }
            else
            {
                return View(account);
            }
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult LoginAuthentication(Account? account)
        {
            //ViewBag.UserName= account.UserName;
            //ViewBag.Password = account.Password;
            using (var context = new AppDbContext(Helper._configurationPub))
            {
                TempData["valid"] = true;


                if (context.Accounts.Where(x => x.UserName == account.UserName).Where(x => x.Password == account.Password).Any())
                {
                    TempData["valid"] = true;
                    HttpContext.Response.Cookies.Append("UserName", account.UserName);
                    HttpContext.Response.Cookies.Append("ID", context.Accounts.FirstOrDefault(x=>x.UserName==account.UserName).Id.ToString());

                    return RedirectToAction("Index", "Books");
                }
                else
                {
                    TempData["valid"] = false;
                    return View("LoginP", account);
                }
            }



        }
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            string name = "";
            if (file != null && file.Length > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine("images/", filename);

                // Check if the photo is already used by any other user
                using (var context = new AppDbContext(Helper._configurationPub))
                {
                    var existingAccountWithPhoto = context.Accounts.FirstOrDefault(x => x.Uploaded_Photo_URl == path);
                    if (existingAccountWithPhoto != null)
                    {
                        // Photo is already used, return an error or handle it as you wish
                        ViewBag.ImgUploaded = false;
                        ViewBag.ErrorMessage = "This photo is already used by another user.";
                        return RedirectToAction("UserAccountMethod");
                    }
                }

                // Photo is not used, proceed with uploading
                using (var context = new AppDbContext(Helper._configurationPub))
                {
                    var account = context.Accounts.FirstOrDefault(x => x.UserName == HttpContext.Request.Cookies["UserName"].ToString());
                    account.Uploaded_Photo_URl = path;
                    context.SaveChanges();
                }

                // Save the file
                var FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);
                using (var stream = new FileStream(FullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                ViewData["ImageUrl"] = filename;
                name = filename;
                ViewBag.ImgUploaded = true;
            }
            else
            {
                ViewBag.ImgUploaded = false;
            }

            return RedirectToAction("UserAccountMethod", new { Sname = name });
        }
        public IActionResult SaveChanges(Account account)
        {
            using(var context = new AppDbContext(Helper._configurationPub))
            {
                context.Accounts.Update(account);
                context.SaveChanges();
                return RedirectToAction("UserAccountMethod");
            }
        }

        public IActionResult UserAccountMethod(string Sname)
        {
            ViewBag.ImgUploaded = false;
            ViewData["ImageUrl"] = Sname;
            string name = HttpContext.Request.Cookies["UserName"].ToString();
            using(var context = new AppDbContext(Helper._configurationPub))
            {
                Account UserAccount =context.Accounts.FirstOrDefault(x => x.UserName == name);
           
            if (UserAccount == null)
            {
                return NotFound();
            }

            return View(UserAccount);
            }
        }
        public IActionResult SignupForm(Account account)
        {

            return View(account);


        }
        [HttpPost]
        public IActionResult SignupConfirm(Account account)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AppDbContext(Helper._configurationPub))
                {
                    context.Accounts.Add(account);
                    context.SaveChanges();
                    return RedirectToAction("LoginP", new { id = account.Id });
                }
            }
        
                return View("SignupForm", account);
            
        }


    }

}

