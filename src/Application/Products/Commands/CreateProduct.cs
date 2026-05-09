using System;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands;

public class CreateProduct
{
    public class Command : IRequest<string>
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class Handler(IProductRepository productRepository) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                CategoryId = request.CategoryId
            };


            return await productRepository.CreateProductAsync(product, cancellationToken);
        }
    }


}
