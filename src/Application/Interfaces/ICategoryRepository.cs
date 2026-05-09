using System;
using Domain.Entities;

namespace Application.Interfaces;

public interface ICategoryRepository
{

    Task<string> CreateCategoryAsync(Category category, CancellationToken cancellationToken = default);
    Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default);
    Task DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default);


}
