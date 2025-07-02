﻿namespace E_Ticaret.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public Guid? CreatedUser { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? UpdatedUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
