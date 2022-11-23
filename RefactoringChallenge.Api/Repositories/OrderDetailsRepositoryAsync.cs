using RefactoringChallenge.Data;
using RefactoringChallenge.Entities;
using RefactoringChallenge.Mapper;
using RefactoringChallenge.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringChallenge.Repositories
{
    public class OrderDetailsRepositoryAsync : RepositoryBase<OrderDetail>, IOrderDetailsRepositoryAsync
    {
        public OrderDetailsRepositoryAsync(NorthwindDbContext northwindDbContext) : base(northwindDbContext)
        {
        }

        public async Task<OrderDetail> AddProductsToOrderAsync(int orderId, OrderDetailRequest orderDetail)
        {
            var newOrderDetails = 
                new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = orderDetail.ProductId,
                    Discount = orderDetail.Discount,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice,
                };
            
            await UpdateAsync(newOrderDetails);
            return newOrderDetails;
        }
    }
}
