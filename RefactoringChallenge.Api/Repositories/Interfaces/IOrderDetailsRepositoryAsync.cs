using RefactoringChallenge.Entities;
using RefactoringChallenge.Mapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringChallenge.Repositories.Interfaces
{
    public interface IOrderDetailsRepositoryAsync
    {
        Task<OrderDetail> AddProductsToOrderAsync(int orderId, OrderDetailRequest orderDetails);
    }
}
