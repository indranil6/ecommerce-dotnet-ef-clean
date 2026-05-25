using System;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.Orders.Queries;

public class GetOrderByCustomer
{
    public class Query : IRequest<List<OrderDto>>
    {

    }

    public class Handler(IOrderRepository orderRepository, IUserAccessor userAccessor) : IRequestHandler<Query, List<OrderDto>>
    {

        public async Task<List<OrderDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentUserId = userAccessor.GetCurrentUserId() ?? throw new Exception("User not authenticated");

            var orders = await orderRepository.GetOrdersByCustomerIdAsync(currentUserId, cancellationToken);
            return [.. orders.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderItems = [.. o.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    ProductName = oi.Product?.Name ?? "Unknown",
                    PriceAtPurchase = oi.PriceAtPurchase
                })]
            })];
        }
    }
}
