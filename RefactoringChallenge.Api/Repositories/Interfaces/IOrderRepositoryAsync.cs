using RefactoringChallenge.Controllers;
using RefactoringChallenge.Entities;
using RefactoringChallenge.Mapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringChallenge.Repositories.Interfaces
{
    public interface IOrderRepositoryAsync
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(OrderRequest product);
        Task<bool> UpdateOrderAsync(int orderId, IEnumerable<OrderDetailRequest> orderDetails);
        Task<bool> DeleteOrderAsync(Order order);

    }
}
