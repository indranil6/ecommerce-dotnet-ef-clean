using System;
namespace Application.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerId { get; set; } = string.Empty;

    public List<OrderItemDto>? OrderItems { get; set; } = [];

}