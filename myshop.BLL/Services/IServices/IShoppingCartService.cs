using myshop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services.IServices
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCartDto> GetCart(string UserId);
        public Task CreateCart(string UserId);
        public Task AddItemToCart(string UserId, int ProductId);
        public Task IncreaseItemQuantity(int CartItemId);
        public Task DecreaseItemQuantity(int CartItemId);
        public Task DeleteItemFromCart(int CartItemId);
        public Task ClearCart(string UserId);
        public Task UpdateCart(string UserId, int count, decimal TotalPrice);


    }
}
