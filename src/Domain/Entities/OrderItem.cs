using System;

namespace Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; } = new Guid();
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }

    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public decimal PriceAtPurchase { get; set; }
}
