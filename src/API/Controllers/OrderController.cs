using System;
using Application.Orders.Commands;
using Application.Orders.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class OrderController : BaseApiController
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateOrder(CreateOrder.Command command)
    {
        var createOrderResponse = await Mediator.Send(command);
        return Ok(createOrderResponse);
    }

    [HttpGet("customer")]
    [Authorize]
    public async Task<ActionResult> GetOrdersByCustomer(GetOrderByCustomer.Query query)
    {

        var orders = await Mediator.Send(query);
        return Ok(orders);
    }
}
