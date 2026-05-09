using System;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands;

public class CreateCategory
{
    public class Command : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Handler(ICategoryRepository categoryRepository) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name
            };

            return await categoryRepository.CreateCategoryAsync(category, cancellationToken);
        }
    }

}
