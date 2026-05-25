using System;

namespace Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = new Guid();
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }

    // Navigation property
    public required string CustomerId { get; set; }
    public AppUser? Customer { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}
