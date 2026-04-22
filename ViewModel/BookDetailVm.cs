namespace PustokApp.ViewModel
{
    public class BookDetailVm
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public int Code { get; set; }
        public bool InStock { get; set; }
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }

    }
}
