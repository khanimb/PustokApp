using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokApp.Data;
using PustokApp.Models;
using PustokApp.ViewModel;
using System.Diagnostics;

namespace PustokApp.Controllers
{
    public class HomeController(PustokAppDbContext dbContext) : Controller
    {


        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = dbContext.Sliders.ToList(),

                FeaturedBooks = dbContext.Books
                .Include(x => x.BookImages)
                .Include(x => x.Author)
                .Where(x => x.IsFeatured).ToList(),

                NewBooks = dbContext.Books
                .Include(x => x.BookImages)
                .Include(x => x.Author)
                .Where(x => x.IsNew).ToList(),

                DiscountedBooks = dbContext.Books
                .Include(x => x.BookImages)
                .Include(x => x.Author)
                .Where(x => x.DiscountPercent > 0).ToList()
            };

            return View(homeVm);
        }

        public IActionResult Chat()
        {
            var messages = dbContext.ChatMessages
                .OrderByDescending(m => m.Date)
                .Take(20) // Son 20 mesajı gətir
                .Select(m => new ChatMessageVM
                {
                    User = m.User,
                    Text = m.Message,
                    Date = m.Date.ToString("HH:mm")
                }).ToList();

            var model = new ChatVM { OldMessages = messages };
            return View(model);
        }


    }
}

   

