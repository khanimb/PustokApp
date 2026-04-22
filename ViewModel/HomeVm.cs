using PustokApp.Models;

namespace PustokApp.ViewModel
{
    public class HomeVm
    {
        public List<Slider> Sliders { get; set; } = null!;
        public List<Book> NewBooks { get; set; } = null!;
        public List<Book> DiscountedBooks { get; set; } = null!;
        public List<Book> FeaturedBooks { get; set; } = null!;
    }
}
