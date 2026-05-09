using System;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProducts
{
    public class Query : IRequest<List<ProductDto>>
    {
        public Guid CategoryId { get; set; } = Guid.Empty;
    }

    public class Handler(IProductRepository productRepository) : IRequestHandler<Query, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetProductsAsync(cancellationToken);

            var filteredProducts = request.CategoryId == Guid.Empty
                ? products
                : [.. products.Where(p => p.CategoryId == request.CategoryId)];

            return [.. filteredProducts.Select(MapToProductDto)];
        }

        private static ProductDto MapToProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty
            };
        }
    }
}
