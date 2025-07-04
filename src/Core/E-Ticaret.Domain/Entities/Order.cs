﻿namespace E_Ticaret.Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; }
}

