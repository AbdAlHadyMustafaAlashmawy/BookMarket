using BookMarket.Models;
using BookMarket.Models.Helpers;
using BookMarket.Repos;
using BookMarket.Repos.Repo_Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookMarket.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProducersRepository _producersRepo;
        private readonly IBooksReposatory _booksRepo;
        public ProducersController(AppDbContext context,IBooksReposatory booksRepo,IProducersRepository producerRepo)
        {
            _context= context;
            _producersRepo = producerRepo;
            _booksRepo = booksRepo;
        }
        public async Task<IActionResult> Index()
        {
            var Producers = await _producersRepo.GetAllAsync();
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
                    _producersRepo.Insert(producer);
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
                var producerID = _producersRepo.GetIdByBookId(BookId);

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

                    bag.cost = (double)_booksRepo.GetById(BookId).Cost;

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
            Producer producer = _producersRepo.GetById(id);
            _producersRepo.Remove(producer);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> View(int id)
        {
            var entity=await _context.Producers.Include(x=>x.Books).ThenInclude(x=>x.Writer).FirstOrDefaultAsync(x => x.Id == id);
            return View(entity);
        }
        public async Task<IActionResult> EditProducer(int id)
        {
            var entity = await _producersRepo.GetByIdAsync(id);
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
                _producersRepo.Update(producer);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditProducer",producer);
            }
        }

    }
}
