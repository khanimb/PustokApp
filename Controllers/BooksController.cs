using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokApp.Data;
using PustokApp.ViewModel;

namespace PustokApp.Controllers
{
    public class BooksController(AppDbContext dbContext) : Controller
    {
        public IActionResult Details(Guid id)
        {
            var book = dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BookImages)
                .Include(b => b.BookTags)
                .ThenInclude(b => b.Tag)
                .FirstOrDefault(b => b.Id == id);

            BookVm bookvm = new()
            {
                Book = book,
                RelatedBooks = dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BookImages)
                .Where(b => b.Id != id && b.AuthorId == book.AuthorId)
                .Take(4)
                .ToList()

            };

            return View(bookvm);
        }


        public IActionResult BookModal(Guid id)
        {
            var book = dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.BookImages)
                .Include(b => b.BookTags)
                .ThenInclude(b => b.Tag)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            return PartialView("_BookModalPartialView", book);

        }
    }
}

