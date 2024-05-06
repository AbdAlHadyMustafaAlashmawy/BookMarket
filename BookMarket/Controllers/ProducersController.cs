using BookMarket.Models;
using BookMarket.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookMarket.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _context;
        public ProducersController(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            var Producers = await _context.Producers.ToListAsync();
            return View(Producers);
        }
        public IActionResult CreateProducerForm(Producer producer)
        {
                return View(producer);
           
        }
        public IActionResult CreateProducer(Producer producer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Producers.Add(producer);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error in confirming field in database");
                    throw;
                }
               

            }
            return View("CreateProducerForm", producer);

        }
        public IActionResult AddOrder(int BookId)
        {
            using (var context = new AppDbContext(Helper._configurationPub))
            {
                var producerID = context.Books.FirstOrDefault(x => x.Id == BookId).ProducerId;

                Bag bag = new Bag() { BookId = BookId };
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

                    bag.cost = (double)context.Books.FirstOrDefault(x => x.Id == BookId).Cost;

                    context.Bag.Add(bag);
                    context.SaveChanges();
                    TempData["Notification"] = "Order added successfully.";
                    return RedirectToAction("View", "Producers", new { id = producerID });


                }
                else
                {
                    TempData["Notification"] = "failed";
                    return RedirectToAction("View", "Producers", new { id = producerID });
                }
            }
        }
        public  IActionResult DeleteProducer(int id)
        {
            var entity = _context.Producers.FirstOrDefault(x => x.Id == id);
            _context.Producers.Remove(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> View(int id)
        {
            var entity=await _context.Producers.Include(x=>x.Books).ThenInclude(x=>x.Writer).FirstOrDefaultAsync(x => x.Id == id);
            return View(entity);
        }
        public async Task<IActionResult> EditProducer(int id)
        {
            var entity =await _context.Producers.FirstOrDefaultAsync(x => x.Id == id);
            return View(entity);
        }
        [HttpPost]
        public IActionResult ConfirmEditProducer(Producer producer)
        {
            ModelState.Remove("Name");
            if (ModelState.IsValid)
            {
                if (producer == null)
                {
                    return NotFound();
                }
                _context.Producers.Update(producer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditProducer",producer);
            }
        }

    }
}
