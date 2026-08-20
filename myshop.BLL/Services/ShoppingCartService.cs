using AutoMapper;
using Microsoft.AspNetCore.Http;
using myshop.BLL.DTO;
using myshop.BLL.ExtensionMethods;
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
    internal class ShoppingCartService : IShoppingCartService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private ICartRepo _cartRepo;
        private ICartItemRepo _cartItemRepo;
        private IProductRepo _productRepo;
        private IMapper _mapper;
        public ShoppingCartService(IHttpContextAccessor contextAccessor,
            ICartRepo cartRepo,
            ICartItemRepo cartItemRepo,
            IProductRepo productRepo,
            IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }
        private ISession? Session => _contextAccessor.HttpContext?.Session;

        public async Task<ShoppingCartDto> GetCart(string UserId)
        {
            var cart = Session?.Get<ShoppingCartDto>(UserId);
            if (cart == null)
            {
                cart = _mapper.Map<ShoppingCartDto>(await _cartRepo.GetCartByUser(UserId));
                if (cart == null)
                    throw new KeyNotFoundException("there is no Shopping Cart To this User");
                Session.Set<ShoppingCartDto>(UserId, cart);
            }

            return cart;

        }
        public async Task CreateCart(string UserId)
        {
            var cart = new ShoppingCart()
            {
                ApplicationUserId = UserId
            };
            await _cartRepo.AddCart(cart);
            await _cartRepo.Save();
            await UpdateCart(UserId, 0, 0);
        }
        public async Task AddItemToCart(string UserId, int ProductId)
        {

            var Cart = await _cartRepo.GetCartByUser(UserId);
            if (Cart == null)
                throw new Exception("there is no Shopping Cart To this User");

            var cartItem = Cart.CartItems?.FirstOrDefault(c => c.ProductId == ProductId);
            if (cartItem != null)
            {
                await IncreaseItemQuantity(cartItem.Id);
            }
            else
            {
                var product = await _productRepo.GetById(ProductId);
                if (product == null)
                    throw new Exception("Product with this Id not Found");
                await _cartItemRepo.Add(new CartItem()
                {
                    ProductId = ProductId,
                    ShoppingCartId = Cart.Id,
                    Quantity = 1

                });
                await _cartItemRepo.SaveChanges();

                await UpdateCart(UserId,
                    Cart.Count + 1,
                    Cart.TotalPrice + product.Price);
            }

        }

        public async Task IncreaseItemQuantity(int CartItemId)
        {
            var cartItem= await _cartItemRepo.GetById(CartItemId ,c => c.ShoppingCart, c => c.product);
            if (cartItem == null) 
                throw new Exception("there is no Cart Item with this Id");
            cartItem.Quantity += 1;
            _cartItemRepo.Update(cartItem);
            await _cartItemRepo.SaveChanges();
            var cart = cartItem.ShoppingCart;

            await UpdateCart(cart.ApplicationUserId,
                cart.Count + 1,
                cart.TotalPrice + cartItem.product?.Price??0);

        }

        public async Task DecreaseItemQuantity(int CartItemId)
        {
            var cartItem = await _cartItemRepo.GetById(CartItemId, c => c.ShoppingCart, c => c.product);
            if (cartItem == null)
                throw new Exception("there is no Cart Item with this Id");
            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                _cartItemRepo.Update(cartItem);
                await _cartItemRepo.SaveChanges();
                var cart = cartItem.ShoppingCart;

                await UpdateCart(cart.ApplicationUserId,
                    cart.Count - 1,
                    cart.TotalPrice - cartItem.product.Price);
            }
            else
                await DeleteItemFromCart(CartItemId);
        }

        public async Task DeleteItemFromCart(int CartItemId)
        {
            var cartItem = await _cartItemRepo.GetById(CartItemId, c => c.ShoppingCart, c => c.product);
            if (cartItem == null)
                throw new Exception("there is no Cart Item with this Id");
            await _cartItemRepo.Delete(cartItem);
            var cart = cartItem.ShoppingCart;
            await _cartItemRepo.SaveChanges();

            await UpdateCart(cart.ApplicationUserId,
                cart.Count-cartItem.Quantity,
                cart.TotalPrice - cartItem.Quantity*cartItem.product.Price);
        }
        public async Task ClearCart(string UserId)
        {
           var cart = await _cartRepo.GetCartByUser(UserId);
            if (cart == null)
                throw new Exception("there is no Shopping Cart To this User");
            
            if (cart.CartItems != null) {
                foreach (var item in cart.CartItems)
                {
                    await _cartItemRepo.Delete(item);

                }
            }
            await _cartItemRepo.SaveChanges();
            await UpdateCart(UserId, 0, 0);
        }
        public async Task UpdateCart(string UserId, int count, decimal TotalPrice)
        {
            var cart = await _cartRepo.GetCartByUser(UserId);
            if (cart == null)
                throw new Exception("there is no Shopping Cart To this User");
            cart.Count = count;
            cart.TotalPrice=TotalPrice;
            _cartRepo.Update(cart);
            await _cartRepo.Save();
            Session?.Set<ShoppingCartDto>(UserId, _mapper.Map<ShoppingCartDto>(cart));

        }




    }
}
