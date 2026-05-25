using System;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands;

public class CreateCategory
{
    public class Command : IRequest<Category>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Handler(ICategoryRepository categoryRepository) : IRequestHandler<Command, Category>
    {
        public async Task<Category> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name
            };

            return await categoryRepository.CreateCategoryAsync(category, cancellationToken);
        }
    }

}
