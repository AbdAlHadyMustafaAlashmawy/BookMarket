using BookMarket.Models;
using BookMarket.Models.DTOs;
using BookMarket.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace BookMarket.Controllers
{
    public class BagController:Controller
    {
        [HttpGet("count")]
        public ActionResult<int> GetBagElementsNum()
        {
            using (AppDbContext context = new AppDbContext(Helper._configurationPub))
            {
                int count = context.Bag.Count();
                return Ok(count);
            }
        }

        public IActionResult Index(Bag bag)
        {
            using (var context = new AppDbContext(Helper._configurationPub))
            {
                var name = HttpContext.Request.Cookies["UserName"];
                if (name == null)
                {
                    // Handle the case when the cookie is not found or user is not logged in
                    return RedirectToAction("Login", "Account");
                }

                var groupedOrders = context.Bag
                    .Include(x => x.Books)
                    .Include(x => x.Account)
                    .Where(x => x.Account.UserName == name)
                    .GroupBy(x => x.BookId)
                    .Select(g => new GroupedOrderByIdDTO
                    {
                        BookId = g.Key,
                        Count = g.Count(),
                        Orders = g.ToList()
                    })
                    .ToList();

                return View(groupedOrders);
            }
        }

        public IActionResult DeleteBook(int id)
        {
            using (var context = new AppDbContext(Helper._configurationPub))
            {
                var element =context.Bag.Find(id);
                context.Bag.Remove(element);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // Action to get count of items in the bag
        public IActionResult GetBagItemCount()
        {
            var name = HttpContext?.Request?.Cookies["UserName"]?.ToString();
            if (name != null)
            {
                using (var context = new AppDbContext(Helper._configurationPub))
                {
                    var itemCount = context.Bag
                        .Include(x => x.Books)
                        .Include(x => x.Account)
                        .Where(x => x.Account.UserName == name).ToList().Count();

                    return Json(new { itemCount });
                }
            }

            return Json(new { itemCount = 0 });
        }

        public IActionResult AddOrder(int BookId)
        {
            Bag bag = new Bag() { BookId=BookId};
            if (HttpContext.Request.Cookies["ID"] != null && int.TryParse(HttpContext.Request.Cookies["ID"].ToString(), out int accountId))
            {
                if (accountId != 0)
                {
                    bag.AccountId = accountId;
                    
                }
                else
                {
                    return BadRequest("Invalid or missing account ID.");
                }
                using (var context = new AppDbContext(Helper._configurationPub))
                {
                    bag.cost =(double)context.Books.FirstOrDefault(x => x.Id == BookId).Cost;
                    
                    context.Bag.Add(bag);
                    context.SaveChanges();
                    TempData["Notification"] = "Order added successfully.";
                    return RedirectToAction("Index","Books");
                }

            }
            else
            {
                TempData["Notification"] = "failed";
                return RedirectToAction("Index", "Books");
            }

        }
    }
}
