using PustokApp.Models;

namespace PustokApp.ViewModel
{
    public class BookVm
    {
        public Book Book { get; set; } = null!;
        public List<Book> RelatedBooks { get; set; } = null!;
    }
}
