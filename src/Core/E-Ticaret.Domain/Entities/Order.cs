namespace E_Ticaret.Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

}

