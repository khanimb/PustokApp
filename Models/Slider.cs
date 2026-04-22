using PustokApp.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokApp.Models
{
    public class Slider : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string ButtonText { get; set; } = null!;
        public string ButtonUrl { get; set; } = null!;
        [NotMapped]
        public IFormFile File { get; set; } = null!;
    }
}

