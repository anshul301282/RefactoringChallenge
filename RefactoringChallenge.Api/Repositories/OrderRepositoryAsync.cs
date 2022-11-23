using Microsoft.EntityFrameworkCore;
using RefactoringChallenge.Controllers;
using RefactoringChallenge.Data;
using RefactoringChallenge.Entities;
using RefactoringChallenge.Mapper;
using RefactoringChallenge.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactoringChallenge.Repositories
{
    public class OrderRepositoryAsync : RepositoryBase<Order>, IOrderRepositoryAsync
    {
        public OrderRepositoryAsync(NorthwindDbContext northwindDbContext) : base(northwindDbContext)
        {
        }
        public async Task<Order> CreateOrderAsync(OrderRequest order)
        {
            var newOrderDetails = new List<OrderDetail>();
            foreach (var orderDetail in order.OrderDetails)
            {
                newOrderDetails.Add(new OrderDetail
                {
                    ProductId = orderDetail.ProductId,
                    Discount = orderDetail.Discount,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice,
                });
            }

            var newOrder = new Order
            {
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = DateTime.Now,
                RequiredDate = order.RequiredDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry,
                OrderDetails = newOrderDetails,
            };
            var neworder = await AddAsync(newOrder);
            return neworder;
        }

        public async Task<bool> DeleteOrderAsync(Order order)
        {
            await DeleteAsync(order);
            return true;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var orderList = await GetAsync(order => order.OrderId == id, null, "OrderDetails", true);
            return orderList.FirstOrDefault();
        }

        public  Task<bool> UpdateOrderAsync(int orderId, IEnumerable<OrderDetailRequest> orderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var orderList = await _northwindDbContext.Orders.Include("OrderDetails").ToListAsync();
            return orderList;
        }
    }
}
