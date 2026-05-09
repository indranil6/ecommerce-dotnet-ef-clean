using System;
using System.Reflection.Metadata.Ecma335;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Queries;

public class GetCategories
{
    public class Query : IRequest<List<CategoryDto>>
    {
    }

    public class Handler(ICategoryRepository categoryRepository) : IRequestHandler<Query, List<CategoryDto>>
    {

        public async Task<List<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.GetCategoriesAsync(cancellationToken);
            return [.. categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = [.. c.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category?.Name ?? string.Empty
                })]
            })];
        }
    }
}
