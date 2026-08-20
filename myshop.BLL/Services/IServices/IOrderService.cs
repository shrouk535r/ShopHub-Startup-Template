using myshop.BLL.DTO;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services.IServices
{
    public interface IOrderService
    {
        public Task<CheckoutDto> Checkout(string UserId);
        public Task TransformFromCartToOrder(ShoppingCart cart, int OrderId);
        public Task PlaceOrder(string UserId, CheckoutDto checkout);
        public Task<IEnumerable<OrderDto>> GetOrders();
        public Task<OrderDetailsDto> GetOrder(int OrderId);
    }
}
