using BookMarket.Models.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BookMarket.Controllers
{
    public class ValidationController:Controller
    {
        
        [HttpPost]
        public IActionResult UniqueProducerName(string Name)
        {
            var IsUnique = new AppDbContext(Helper._configurationPub).Producers.Any(x => x.Name == Name);
            return Json(IsUnique);
        }
    }
}
