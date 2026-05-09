using System;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductsByCategory
{

    public class Query : IRequest<List<ProductDto>>
    {
        public Guid CategoryId { get; set; }
    }

    public class Handler(IProductRepository productRepository) : IRequestHandler<Query, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetProductsByCategoryAsync(request.CategoryId, cancellationToken);
            List<ProductDto> productDtos = [.. products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            })];
            return productDtos;
        }
    }

}
