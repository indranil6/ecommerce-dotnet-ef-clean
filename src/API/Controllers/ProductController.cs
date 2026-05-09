using System;
using Application.DTOs;
using Application.Products.Commands;
using Application.Products.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class ProductController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProduct(Product product)
    {
        var command = new CreateProduct.Command
        {
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId
        };

        var productId = await Mediator.Send(command);
        return Ok(productId);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery] Guid? categoryId)
    {
        var query = new GetProducts.Query
        {
            CategoryId = categoryId ?? Guid.Empty
        };
        var products = await Mediator.Send(query);
        return Ok(products);
    }
}
