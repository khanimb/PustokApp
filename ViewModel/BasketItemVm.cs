using System.Text.Json;

namespace PustokApp.ViewModel;

public class BasketItemVm
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public decimal BookPrice { get; set; }
    public int Count { get; set; }
    public string MainImageUrl { get; set; }
}
