using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopBerry.Data;
using ShopBerry.Models;
using ShopBerry.Models.Dtos;

namespace ShopBerry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrdersController : ControllerBase // Лучше использовать ControllerBase для API
    {
        private readonly ShopContext _context;

        public OrdersController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet] // Получить все заказы
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ToListAsync();

            return Ok(orders); // Возвращаем список заказов в формате JSON
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")] // Получить заказ по ID
                          // Не обязательно дублировать авторизацию
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order); // Возвращаем заказ в формате JSON
        }

        // POST: api/orders/complete/{id}
        [HttpPost("complete/{id}")] // Завершить заказ по ID

        public async Task<IActionResult> CompleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.IsCompleted = true; // Установка статуса заказа
            await _context.SaveChangesAsync();

            return NoContent(); // Возвращаем 204 No Content после успешного завершения
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOdrerDto orderDto)
        {
            if (ModelState.IsValid)
            {


                var customerExists = await _context.Customers.AnyAsync(c => c.Id == orderDto.CustomerId);
                if (!customerExists)
                {
                    return BadRequest("Customer with the provided ID does not exist.");
                }

                var order = new Order
                {
                    Id = orderDto.Id,
                    OrderNumber = orderDto.OrderNumber,
                    IsCompleted = orderDto.IsCompleted,
                    CreatedAt = orderDto.CreatedAt,
                    CustomerId = orderDto.CustomerId,
                    OrderItems = orderDto.OrderItems.Select(item => new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    }).ToList()
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Details), new { id = order.Id }, order);
            }

            return BadRequest(ModelState);
        }
    }
}
