using System;

namespace Domain.Entities;

public class Category
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = [];

}
