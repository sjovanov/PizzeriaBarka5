using Newtonsoft.Json;
using PicerijaBarka5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PicerijaBarka5.Helpers
{
    public static class HtmlHelpers
    {
        public static string SetActiveClass(this HtmlHelper helper, string controller, string compareController, string action = "", string compareAction = "")
        {
            if (String.IsNullOrEmpty(action)) { 
                return controller == compareController ? "active" : "";
            } else
            {
                return action == compareAction && controller == compareController ? "active" : "";
            }
        }

        public static string GetOrderRequestJson(this HtmlHelper helper, CartOrder cart)
        {
            var request = new OrderRequest();

            foreach (var item in cart.Items)
            {
                request.Items.Add(item.Key.PizzaId, item.Value);
            }

            var a = JsonConvert.SerializeObject(request, Formatting.None);

            return a;
        }
    }
}