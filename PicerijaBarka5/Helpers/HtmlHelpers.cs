using Newtonsoft.Json;
using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
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
            if (String.IsNullOrEmpty(action))
            {
                return controller == compareController ? "active" : "";
            }
            else
            {
                return action == compareAction && controller == compareController ? "active" : "";
            }
        }

        public static string GetOrderRequestJson(this HtmlHelper helper, CartOrderDto cart)
        {
            var items = new List<CartItemDto>();
            if (cart != null)
            {
                items = cart.Items;
            }
            return JsonConvert.SerializeObject(cart, Formatting.None);
        }

        public static string SetCheckedAttribute(this HtmlHelper helper, ICollection<IngredientDto> ingredients, Guid id)
        {
            return ingredients.Any(x => x.IngredientId == id) ? "checked" : "";
        }

        public static string SetOrderStatusClass(this HtmlHelper helper, string status)
        {
            if (status == OrderStatus.Accepted)
            {
                return "text-primary";
            }
            else if (status == OrderStatus.InProgress)
            {
                return "text-warning";
            }
            else if (status == OrderStatus.InDelivery)
            {
                return "text-info";
            }
            else if (status == OrderStatus.Declined)
            {
                return "text-danger";
            }
            else
            {
                return "text-success";
            }
        }

        public static string SetIngredientStatusClass(this HtmlHelper helper, IngredientDto ingredient)
        {
            if(ingredient.isInStock())
            {
                return "text-success";
            } else
            {
                return "text-danger";
            }
        }
    }
}