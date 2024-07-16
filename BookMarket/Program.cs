using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using BookMarket.Models.Helpers;
using BookMarket.Repos.Repo_Interfaces;
using BookMarket.Repos;

namespace BookMarket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Your data insertion methods
            //Helper.ProducersDataInsertion();
            //Helper.WritersDataInsertion();
            //Helper.BooksDataInsertion();

            // Create the web host builder
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddScoped<IBooksReposatory, BooksRepository>();
            builder.Services.AddScoped<IWritersRepository, WritersRepository>();
            builder.Services.AddScoped<IProducersRepository, ProducersRepository>();
            builder.Services.AddScoped<IBagsReposatory, BagsReposatory>();
            // Register the AppDbContext with dependency injection.
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
            // Build the application.
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name:"MyRoute", "DisplayMyBag/{action=Index}",
                new { controller="Bag"}
                );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=LoginP}");

            app.Run();
        }
    }
}
