﻿using Ecomerce_Web.Helpers;
using Ecomerce_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce_Web.ViewComponents
{
    public class CartViewComponent : ViewComponent 
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySettingConst.CART_KEY) ?? new List<CartItem>();


            return View("CartPanel", new CartModel
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)
            });

        }
    }
}
