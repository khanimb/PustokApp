using PustokApp.Models.Common;
using PustokApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokApp.Models
{
    public class Book : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }

        public int Code { get; set; }
        public bool InStock { get; set; }
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }
        public string MainImageUrl { get; set; } = null!;
        public string HoverImageUrl { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public List<BookImage> BookImages { get; set; } = null!;
        public List<BookTag> BookTags { get; set; } = null!;
        [NotMapped]
        public List<Guid> TagIds { get; set; } = null!;
        [NotMapped]
        public List<IFormFile> Files { get; set; } = null!;
        [NotMapped]
        public IFormFile MainImage { get; set; } = null!;
        [NotMapped]
        public IFormFile HoverImage { get; set; } = null!;

    }
}

