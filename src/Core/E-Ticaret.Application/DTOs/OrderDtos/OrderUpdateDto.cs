﻿namespace E_Ticaret.Application.DTOs.OrderDtos;

public record class OrderUpdateDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
}