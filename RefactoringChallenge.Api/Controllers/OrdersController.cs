using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefactoringChallenge.Mapper;
using RefactoringChallenge.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactoringChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IOrderDetailsRepositoryAsync _orderDetailsRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMapper mapper, IOrderRepositoryAsync orderRepository, IOrderDetailsRepositoryAsync orderDetailsRepository, ILogger<OrdersController> logger)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = await _orderRepository.GetOrdersAsync();
            var result = _mapper.Map<IEnumerable<OrderResponse>>(query);
            return Ok(result);
        }


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById([FromRoute] int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogError($"Order with id: {orderId}, not found.");
                return NotFound();
            }
            var result = _mapper.Map<OrderResponse>(order);

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] OrderRequest order)
        {
            var newOrder = await _orderRepository.CreateOrderAsync(order);
            return Ok(_mapper.Map<OrderResponse>(newOrder));
        }

        [HttpPost("{orderId}/[action]")]
        public async Task<IActionResult> AddProductsToOrder([FromRoute] int orderId, OrderDetailRequest orderDetails)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogError($"Order with id: {orderId}, not found.");
                return NotFound();
            }
            var newOrderDetails = await _orderDetailsRepository.AddProductsToOrderAsync(orderId, orderDetails);
            return Ok(_mapper.Map<OrderDetailResponse>(newOrderDetails));
        }

        [HttpPost("{orderId}/[action]")]
        public async Task<IActionResult> Delete([FromRoute] int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogError($"Order with id: {orderId}, not found.");
                return NotFound();
            }
            await _orderRepository.DeleteOrderAsync(order);
            return Ok(true);
        }
    }
}
