using BookMarket.Models;
using BookMarket.Models.ModelSpecialConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Reflection;
namespace BookMarket
{

        public class AppDbContext : DbContext
        {
        private IConfiguration _Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public AppDbContext(IConfiguration configuration,DbContextOptions options):base(options)
        {
            _Configuration = configuration;
        }
        public DbSet<Bag> Bag { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Book> Books { get; set; }
            public DbSet<Producer> Producers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Conn").Value;
            optionsBuilder.UseSqlServer(conn);


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }

    }

}