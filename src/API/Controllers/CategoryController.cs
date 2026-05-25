using System;
using Application.Categories.Commands;
using Application.Categories.Queries;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        var createdCategory = await Mediator.Send(new CreateCategory.Command
        {
            Name = category.Name
        });
        return Ok(new
        {
            Message = "Category created successfully",
            Category = createdCategory
        });
    }
}
