using BookMarket.Models;
using BookMarket.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PagedList;
using PagedList.Mvc;

namespace BookMarket.Controllers
{
    public class BooksController:Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Books = await _context.Books.Include(x=>x.Writer).Include(x=>x.Producer).ToListAsync();
            TempData["books"] = _context.Books.ToList();

            return View(Books);
        }
        public IActionResult View(int id)
        {
            var book = _context.Books.Include(x=>x.Producer).Include(x=>x.Writer).FirstOrDefault(x => x.Id == id);
            return View(book);
        }
        public IActionResult ConfirmAddingBook(Book book)
        {
            //TempData["Producer1"] = _context.Producers.ToList();
            //TempData["Writer1"] = _context.Writers.ToList();
            ViewBag.book = book;
            TempData["Producer"] = _context.Producers.ToList();
            TempData["Writer"] = _context.Writers.ToList();
            var books = _context.Books.ToList();
            if (book is null)
            {
                return View("NotFound");

            }
            if (!ModelState.IsValid)
            {
                return View("AddNewBook",books);
            }
            else
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("CRUD");
            }

        }
        public IActionResult DeleteBook(int id)
        {
           var book= _context.Books.FirstOrDefault(x => x.Id == id);
            _context.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("CRUD");
        }
        public IActionResult EditBook(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            TempData["Producer"] = _context.Producers.ToList();
            TempData["Writer"] = _context.Writers.ToList();
            return View(book);
        }
        public IActionResult ConfirmEditingBooks(Book book)
        {
     
            if (ModelState.IsValid)
            {
            _context.Books.Update(book);
            _context.SaveChanges();
            return RedirectToAction("CRUD");
            }
            else
            {
                return View("AddNewBook",book);

            }
        }
        public IActionResult AddNewBook()
        {
            var books = _context.Books.Include(x=>x.Writer).Include(x=>x.Producer).ToList();
            TempData["Producer"] = _context.Producers.ToList();
            TempData["Writer"] = _context.Writers.ToList();
            return View(books);
        }
        public async Task<IActionResult> CRUD(string Kind,int MaxCost=1000000,int MinCost=0,string Name= null,int pageNo= 1,int NoOfRecordsPerPage=0)
        {
            IQueryable<Book> booksQuery = _context.Books;
            TempData["books"] = _context.Books.ToList();
            if (!string.IsNullOrEmpty(Kind))
            {
                if (Enum.TryParse<BookCategory>(Kind, out BookCategory category))
                {
                    booksQuery = booksQuery.Where(x => x.Kind == category);
                }
            }

            booksQuery = booksQuery.Where(x => x.Cost >= MinCost && x.Cost <= MaxCost);
            if (Name != null)
            {
                booksQuery = booksQuery.Where(x => x.Name.Contains(Name));
            }
            var books = await booksQuery.ToListAsync();
            if(NoOfRecordsPerPage == null || NoOfRecordsPerPage == 0)
            {
                NoOfRecordsPerPage = 8;
            }
            var NoOfRecordsToSkip = (pageNo - 1) * NoOfRecordsPerPage;
            int NoOfPages = (int)Math.Ceiling((decimal)books.Count() / (decimal)NoOfRecordsPerPage);
            ViewBag.PageNo = pageNo;
            ViewBag.NoOfPages = NoOfPages;
            ViewBag.Kind = Kind;
            ViewBag.MaxCost = MaxCost;
            ViewBag.MinCost = MinCost;
            ViewBag.Name = Name;
            ViewBag.NoOfRecordsPerPage = NoOfRecordsPerPage;
            books = books.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
            return View(books);

        }
        [HttpPost]
        public async Task<IActionResult> FiltersForIndex(string Kind, int MaxCost = 1000000, int MinCost = 0, string Name = null)
        {
            IQueryable<Book> booksQuery = _context.Books.Include(x=>x.Producer).Include(x=>x.Writer);

            if (!string.IsNullOrEmpty(Kind))
            {
                if (Enum.TryParse<BookCategory>(Kind, out BookCategory category))
                {
                    booksQuery = booksQuery.Where(x => x.Kind == category);
                }
            }
            TempData["books"] = _context.Books.ToList();

            booksQuery = booksQuery.Where(x => x.Cost >= MinCost && x.Cost <= MaxCost);
            if (Name != null)
            {
                booksQuery = booksQuery.Where(x => x.Name.Contains(Name));
            }
            var books = booksQuery.ToList();
            return View("Index", books);

        }
    }
}
