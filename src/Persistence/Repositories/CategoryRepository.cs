using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<string> CreateCategoryAsync(Category category, CancellationToken cancellationToken = default)
    {
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync(cancellationToken);
        return category.Id.GetHashCode().ToString();
    }

    public async Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await dbContext.Categories.FindAsync([id], cancellationToken);
        if (category != null)
        {
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.Include(c => c.Products).ToListAsync(cancellationToken);
    }
    public async Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.FindAsync([id], cancellationToken) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
    }


    public async Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default)
    {
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
