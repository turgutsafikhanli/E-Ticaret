using E_Ticaret.Domain.Enums;

namespace E_Ticaret.Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

}

