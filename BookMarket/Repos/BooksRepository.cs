using BookMarket.Models;
using BookMarket.Models.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookMarket.Repos
{
    public class BooksRepository
    {
        private readonly AppDbContext context;

        public BooksRepository()
        {
            context = new AppDbContext(Helper._configurationPub);
        }
        public List<Book> GetWhole()
        {
            return context.Books.Include(x => x.Writer).Include(x => x.Producer).ToList();
        }
        public async Task<List<Book>> GetWholeAsync()
        {
            return await context.Books.Include(x => x.Writer).Include(x => x.Producer).ToListAsync();
        }
        public Book GetById(int id)
        {
            return context.Books.Include(x => x.Producer).Include(x => x.Writer).FirstOrDefault(x => x.Id == id);
        }
        public List<Book> GetAll()
        {
            return context.Books.ToList();
        }
        public void Insert(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }
        public void Remove(Book book)
        {
            context.Books.Remove(book);
            context.SaveChanges();
        }
        public void Update(Book book)
        {
            context.Books.Update(book);
            context.SaveChanges();
        }
    }
}
