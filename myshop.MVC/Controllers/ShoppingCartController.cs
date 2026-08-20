using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Services.IServices;
using Stripe.BillingPortal;
using System.Security.Claims;

namespace myshop.MVC.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private IShoppingCartService _shoppingCartService; 
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(string returnUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                try
                {
                    var cart = _shoppingCartService.GetCart(userId);

                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }

            }
            else
            {
                TempData["ErrorMessage"] = "User Must Login First";

            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: ShoppingCartController/Add Item
        [HttpGet]
        public async Task<IActionResult> AddItem(int ProductId,string returnUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                await _shoppingCartService.AddItemToCart(userId, ProductId);
                TempData["SuccessMessage"] = "Item Added Successfully to Cart";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"]=ex.Message;
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> IncreaseQty(int cartItemId,string returnUrl)
        {
            if (cartItemId == null)
            {
                TempData["ErrorMessage"] = "must login first to increase item";
            }
            try
            {
                await _shoppingCartService.IncreaseItemQuantity(cartItemId);
                TempData["SuccessMessage"] = "Item Increased Successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> DecreaseQty(int cartItemId, string returnUrl)
        {
            if (cartItemId == null)
            {
                TempData["ErrorMessage"] = "must login first to decrease item ";
            }
            try
            {
                await _shoppingCartService.DecreaseItemQuantity(cartItemId);
                TempData["SuccessMessage"] = "Item Decreased Successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> DeleteItem(int cartItemId, string returnUrl)
        {
            if (cartItemId == null)
            {
                TempData["ErrorMessage"] = "must login first to delete item";
            }
            try
            {
                await _shoppingCartService.DeleteItemFromCart(cartItemId);
                TempData["SuccessMessage"] = "Item Deleted Successfully to Cart";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> ClearCart( string returnUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                TempData["ErrorMessage"] = "must login first to Clear Cart";
            }
            try
            {
                await _shoppingCartService.ClearCart(userId);
                TempData["SuccessMessage"] = "Cart Cleared Successfully";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
