using RefactoringChallenge.Entities;
using RefactoringChallenge.Mapper;
using System.Collections.Generic;

namespace APIUnitTest.Data
{
    public static class OrderMockData
    {
        public static List<Order> GetOrderMockData()
        {
            return new List<Order>()
            {
                new Order()
                {
                    CustomerId = "RATTC",
                    EmployeeId = 1,
                    ShipVia = 2,
                    Freight = 8,
                    ShipName = "Rattlesnake Canyon Grocery",
                    ShipAddress = "2817 Milton Dr.",
                    ShipCity =  "Albuquerque",
                    ShipRegion= "NM",
                    ShipPostalCode= "87110",
                    ShipCountry= "USA",
                    OrderId = 1,
                    OrderDetails =  new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                              ProductId= 77,
                              UnitPrice =  13,
                              Quantity =  3,
                              Discount = 0,
                              OrderId = 1,
                        }
                    }
                }
            };
        }

        public static OrderDetailRequest GetOrderDetailsMockData()
        {
            var orderDetails =
                new OrderDetailRequest
                {
                    ProductId = 77,
                    UnitPrice = 13,
                    Quantity = 3,
                    Discount = 0
                };
            return orderDetails;
        }

        public static List<OrderRequest> GetOrderRequestMockData()
        {
            return new List<OrderRequest>()
            {
                new OrderRequest()
                {
                    CustomerId = "RATTC",
                    EmployeeId = 1,
                    ShipVia = 2,
                    Freight = 8,
                    ShipName = "Rattlesnake Canyon Grocery",
                    ShipAddress = "2817 Milton Dr.",
                    ShipCity =  "Albuquerque",
                    ShipRegion= "NM",
                    ShipPostalCode= "87110",
                    ShipCountry= "USA",
                    OrderDetails =  new List<OrderDetailRequest>
                    {
                        new OrderDetailRequest
                        {
                              ProductId= 77,
                              UnitPrice =  13,
                              Quantity =  3,
                              Discount = 0,
                        }
                    }
                }
            };
        }
    }
}
