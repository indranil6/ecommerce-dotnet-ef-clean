using System;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Interfaces;

namespace Persistence.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([id], cancellationToken);
        if (product != null)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Products.FindAsync([id], cancellationToken);
    }


    public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Product>> GetProductsByIdsAsync(List<Guid> productIds, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }
}
