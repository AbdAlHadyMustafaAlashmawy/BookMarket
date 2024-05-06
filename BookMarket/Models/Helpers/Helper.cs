using BookMarket.Models.Enums;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace BookMarket.Models.Helpers
{
    public class Helper
    {
        public Helper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private static IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static IConfiguration _configurationPub;

        public Helper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) // Add IHttpContextAccessor here
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor; // Set the field
        }
        public string CookieUserName
        {
            get
            {
                var httpContextAccessor = new HttpContextAccessor(); // Assuming IHttpContextAccessor is accessible
                var userName = httpContextAccessor.HttpContext?.Request.Cookies["UserName"]?.ToString();
                return userName;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrEmpty(CookieUserName);
            }
        }

        //public string? CookieUserName => _httpContextAccessor?.HttpContext?.Request.Cookies?["UserName"]?.ToString();
        public static int CookieBagElementsNumber;
        //public int GetNumberOfBagElements()
        //{
        //    using (var Context = new AppDbContext(Helper._configurationPub))
        //    {
        //        int BagElementNumber = Context.Bag.Include(x => x.Books).Include(x => x.Account).Where(x => x.Account.UserName == CookieUserName).Count();
        //        CookieBagElementsNumber = BagElementNumber;
        //        return BagElementNumber;
        //    }

        //}
        public static void ProducersDataInsertion()
        {
            using (var context = new AppDbContext(_configuration))
            {
                context.Database.EnsureCreated();
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                {
                    new Producer()
                    {
                        Name = "دار ابن كثير",
                        Description="دار ابن كثير للطباعة والنشر والتوزيع، هي دار نشر عربية أُسست بمدينة دمشق في سوريا، سنة 1403 هـ الموافق لـ 1983م.",
                        LogoUrl="https://upload.wikimedia.org/wikipedia/ar/thumb/f/fd/%D8%AF%D8%A7%D8%B1_%D8%A7%D8%A8%D9%86_%D9%83%D8%AB%D9%8A%D8%B1.jpg/250px-%D8%AF%D8%A7%D8%B1_%D8%A7%D8%A8%D9%86_%D9%83%D8%AB%D9%8A%D8%B1.jpg",
                    },
                     new Producer()
                    {
                        Name = "دار المعرفة",
                        Description="دار المعرفة هي المدرسة الوحيدة في دبي التي تُدرس منهج دبلوما البكالوريا الدولية باللغتين العربية والإنجليزية.",
                        LogoUrl="data:image/png;base64,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",
                    }

                });
                }
                context.SaveChanges();
            }
        }
        public static void WritersDataInsertion()
        {
            using (var context = new AppDbContext(_configuration))
            {
                if (!context.Writers.Any())
                {
                    context.Writers.Add(new Writer
                    {
                        Name = "محمد بن إسماعيل بن إبراهيم بن المغيرة بن بردزبه الجعفي البخاري",
                        BirthDay = new DateTime(810, 7, 20),
                        ProfilePictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4b/ImamBukhari1.png/280px-ImamBukhari1.png",
                        Religion = "Islam",
                        DeadOrAlive = false,
                        Description = "يعتبر الإمام البخاري من أهم علماء الحديث النبوي الشريف عند أهل السنة والجماعة، بل هو أصح كتاب بعد كتاب الله. وقد جمع الإمام البخاري في هذا الكتاب أكثر من 7000 حديث نبوي شريف، وقام بترتيبها حسب الأبواب والفصول."
                    });

                }

                context.SaveChanges();
            }
        }
        public static void BooksDataInsertion()
        {
            using(var context = new AppDbContext(_configuration))
            {
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Name = "صحيح البخاري",
                            Kind=BookCategory.Islamic,
                            ProducerId = 2,
                            Cost = 465,
                            PictureUrl= "https://assets.asfar.io/uploads/2023/12/25152558/sahih-albukhari-book-altaqwa.jpg",
                            WriterId=5,
                            Description="يُعتبر أصح كتاب بعد القرآن الكريم عند أهل السنة والجماعة."
                           ,SellingTimes=15,
                            

                        }

                    });
                    context.SaveChanges();

                }
            }
        }

    }
}
