using PustokApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PustokApp.Models
{
    public class Author : BaseEntity
    {
        [Required]
        public string FullName { get; set; } = null!;
        public List<Book> Books { get; set; } = new();
    }
}

