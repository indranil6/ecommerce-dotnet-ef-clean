using System;

namespace Application.DTOs;

public class CategoryDto
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public List<ProductDto> Products { get; set; } = [];

}
