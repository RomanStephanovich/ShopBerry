using ShopBerry.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBerry.Services.Services.Implementations
{
    internal class OrderControllerService : IOrdersControllerService
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ToListAsync();

            return Ok(orders); 
        }
    }
}
