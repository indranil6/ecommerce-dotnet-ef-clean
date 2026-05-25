namespace Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = new Guid();
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}