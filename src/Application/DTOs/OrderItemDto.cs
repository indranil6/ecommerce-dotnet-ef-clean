using System;

namespace Application.DTOs;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    public required string ProductName { get; set; }

    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}
