using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PustokApp.Data;
using PustokApp.Models;
using PustokApp.Service;
using PustokApp.Setting;
using PustokApp.ViewModel;
using System.Text.Json;

namespace PustokApp.Controllers;

public class BasketController(AppDbContext context) : Controller
{
    public IActionResult AddBasket(Guid id)
    {
        var book = context.Books.Find(id);
        if (book == null)
            return NotFound();
        List<BasketItemVm> basketItems;
        var basketStr = Request.Cookies["pustokSession"];
        if (string.IsNullOrEmpty(basketStr))
        {
            basketItems = new List<BasketItemVm>();
        }
        else
        {
            basketItems = JsonSerializer.Deserialize<List<BasketItemVm>>(basketStr);
        }
        var existbasketItem = basketItems.FirstOrDefault(b => b.BookId == id);
        if (existbasketItem == null)
        {
            basketItems.Add(new BasketItemVm
            {
                BookId = book.Id,
                BookName = book.Name,
                BookPrice = (decimal)(book.DiscountPercent > 0 ? book.Price - (book.DiscountPercent*book.Price/100) : book.Price),
                Count = 1,
                MainImageUrl = book.MainImageUrl
            });
        }
        else
        {
            existbasketItem.Count++;
        }

        if (User.Identity.IsAuthenticated)
        {
            // var user=context.Users
            // .Include(u => u.BasketItems)
            // .FirstOrDefault(u => u.UserName == User.Identity.Name);
        }
        Response.Cookies.Append("pustokSession", JsonSerializer.Serialize(basketItems));


        return PartialView("_BasketPartial", new List<BasketItem>());
    }
    
    public IActionResult SetCookie(string key, string value)
    {
        Response.Cookies.Append("pustokCookie", "pustokk");
        return Content("Cookie set successfully");
    }
    public IActionResult SetSession(string key, string value)
    {
        HttpContext.Session.SetString("pustokSession", "pustokk");
        return Content("Session set successfully");
    }
    public IActionResult GetSession(string key)
    {
        var sessionValue = HttpContext.Session.GetString("pustokSession");
        return Content($"Session value: {sessionValue}");
    }
}
