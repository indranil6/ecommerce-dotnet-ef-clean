using System;
using Domain.Entities;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken);
    Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<string> CreateProductAsync(Product product, CancellationToken cancellationToken);
    Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
    Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);

    Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken);

}
