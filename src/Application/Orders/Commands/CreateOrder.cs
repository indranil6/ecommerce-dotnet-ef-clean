using System;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.VisualBasic;

namespace Application.Orders.Commands;

public class CreateOrder
{
    public class Command : IRequest<OrderDto>
    {
        public List<CreateOrderItemDto> Items { get; set; } = [];
    }

    public class Handler(IOrderRepository orderRepository, IProductRepository productRepository, IUserAccessor userAccessor) : IRequestHandler<Command, OrderDto>
    {
        public async Task<OrderDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = userAccessor.GetCurrentUserId() ?? throw new Exception("User not authenticated");
            var productIds = request.Items.Select(i => i.ProductId).ToList();
            var products = await productRepository.GetProductsByIdsAsync(productIds, cancellationToken);

            if (request.Items.Count == 0)
            {
                throw new Exception("Order must contain at least one item");
            }

            var order = new Order
            {
                CustomerId = userId,
                TotalAmount = 0
            };

            var totalAmount = 0;

            foreach (var item in request.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId) ?? throw new Exception($"Product with ID {item.ProductId} not found");
                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    PriceAtPurchase = product.Price
                };

                totalAmount += (int)Math.Round(product.Price * item.Quantity);
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;
            await orderRepository.CreateOrderAsync(order, cancellationToken);

            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount
            };
        }


    }
}
