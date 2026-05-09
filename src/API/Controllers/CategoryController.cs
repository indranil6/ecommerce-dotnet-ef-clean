using System;
using Application.Categories.Commands;
using Application.Categories.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CategoryController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        var categories = await Mediator.Send(new GetCategories.Query());
        return Ok(categories);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCategory(Category category)
    {
        var categoryId = await Mediator.Send(new CreateCategory.Command
        {
            Name = category.Name
        });
        return Ok(categoryId);
    }
}
