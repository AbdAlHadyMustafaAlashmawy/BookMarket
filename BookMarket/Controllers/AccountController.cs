using BookMarket.Models;
using BookMarket.Models.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.IO;
using System.Net;
using System.Security.Claims;

namespace BookMarket.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult UserAccountMethod(string Sname)
        {
            ViewBag.ImgUploaded = false;
            ViewData["ImageUrl"] = Sname;
            string? name =HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Name").Value;
            if (string.IsNullOrEmpty(name))
            {
                return Unauthorized();
            }

            using (var context = new AppDbContext(Helper._configurationPub))
            {

                Account UserAccount = context.Accounts.FirstOrDefault(x => x.UserName == name);

                if (UserAccount == null)
                {
                    return NotFound();
                }

                return View(UserAccount);
            }
        }


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
                    //HttpContext.Response.Cookies.Append("UserName", account.UserName);
                    //HttpContext.Response.Cookies.Append("ID", context.Accounts.FirstOrDefault(x=>x.UserName==account.UserName).Id.ToString());
                    ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claims.AddClaim(new Claim("UserName", account.UserName));
                    claims.AddClaim(new Claim("ID", context.Accounts.FirstOrDefault(x => x.UserName == account.UserName).Id.ToString()));
                    claims.AddClaim(new Claim("IsAdmin", context.Accounts.FirstOrDefault(x => x.UserName == account.UserName).Admin.ToString()));
                    ClaimsPrincipal principal = new ClaimsPrincipal(claims);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
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
        [ValidateAntiForgeryToken]

        public IActionResult Upload([FromForm] IFormFile file, [FromForm] Account acc)
        {
            using (var _context = new AppDbContext(Helper._configurationPub))
            {


                if (HttpContext.Request.Cookies.TryGetValue("UserName", out string name))
                {
                    var userAccount = _context.Accounts.FirstOrDefault(x => x.UserName == name);

                    if (userAccount == null)
                    {
                        return NotFound();
                    }

                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            file.CopyTo(memoryStream);
                            userAccount.Image = memoryStream.ToArray();
                        }
                    }

                    // Update other properties
                    userAccount.PhoneNumber = acc.PhoneNumber;
                    userAccount.Gmail = acc.Gmail;
                    userAccount.Birthday = acc.Birthday;
                    userAccount.Admin = acc.Admin;
                    userAccount.facebookAcct = acc.facebookAcct;

                    _context.Accounts.Update(userAccount);
                    _context.SaveChanges();

                    ViewBag.ImgUploaded = true;
                }

                else
                {
                    return Unauthorized();
                }

                return RedirectToAction("UserAccountMethod");
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

