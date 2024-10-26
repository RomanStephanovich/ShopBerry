using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBerry.Services.Services.Interfaces
{
    public class IOrdersControllerService
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
