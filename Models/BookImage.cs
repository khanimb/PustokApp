using PustokApp.Models.Common;

namespace PustokApp.Models
{
    public class BookImage : BaseEntity
    {
        public string ImageUrl { get; set; } = null!;
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
