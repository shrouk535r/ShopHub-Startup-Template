using AutoMapper;
using myshop.BLL.DTO;
using myshop.BLL.Services.IServices;
using myshop.DAL.Models;
using myshop.DAL.Repositories.IRepositories;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IOrderItemRepo _orderItemRepo;
        private IOrderRepo _orderRepo;
        private IOrderHeaderRepo _orderHeaderRepo;
        private ICartRepo _cartRepo;
        private IShoppingCartService _cartService;
        private IMapper _mapper;
        public OrderService(
            IOrderItemRepo orderItemRepo,
            IOrderRepo orderRepo,
            IOrderHeaderRepo orderHeaderRepo,
            ICartRepo cartRepo,
            IShoppingCartService cartService,
            IMapper mapper
            )
        {
            _orderItemRepo = orderItemRepo;
            _orderRepo = orderRepo;
            _orderHeaderRepo= orderHeaderRepo;
            _cartRepo = cartRepo;
            _cartService = cartService;
            _mapper = mapper;
        }

        public async Task<CheckoutDto> Checkout(string UserId)
        {
            var cart = await _cartRepo.GetCartByUser(UserId);
            if (cart == null)
                throw new ArgumentException("customer or shopping car Not found");
            if(!cart.CartItems.Any() || cart.CartItems.Count<=0)
                throw new ArgumentException("ShoppingCart is empty");
            var checkout = _mapper.Map<CheckoutDto>(cart);
            return checkout;
        }

        public async Task TransformFromCartToOrder(ShoppingCart cart, int OrderId)
        {
            foreach (var item in cart.CartItems)

            {

                OrderItem orderitem = _mapper.Map<OrderItem>(item);

                orderitem.OrderId = OrderId;

                await _orderItemRepo.AddItem(orderitem);

            }

        }
        public async Task PlaceOrder(string UserId, CheckoutDto checkout)
        {
            var cart = await _cartRepo.GetCartByUser(UserId);
            if (cart == null)
                throw new ArgumentException("customer or shopping car Not found");
            if (!cart.CartItems.Any() || cart.CartItems.Count <= 0)
                throw new ArgumentException("ShoppingCart is empty");
            using var transcation = await _orderRepo.BeginTransaction();

            var orderHeader = _mapper.Map<OrderHeader>(checkout);
            
            orderHeader.UserName= checkout.UserName==null?cart.ApplicationUser.FullName:checkout.UserName;
            
            orderHeader.ApplicationUserId = UserId;
            await _orderHeaderRepo.Add(orderHeader);
            var Order = _mapper.Map<Order>(cart);
            Order.OrderHeaderId = orderHeader.Id;
            await _orderRepo.AddOrder(Order);
            await TransformFromCartToOrder(cart, Order.Id);
            await _cartService.ClearCart(UserId);
            await _orderRepo.SaveChangesAsync();
            await transcation.CommitAsync();
        }
        public async Task<OrderDetailsDto> GetOrder(int OrderId)
        {
            var order = await _orderRepo.GetOrder(OrderId);
            return _mapper.Map<OrderDetailsDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = await _orderRepo.GetOrders();
            return orders.Select(o => _mapper.Map<OrderDto>(o));
        }

        

    }
}
