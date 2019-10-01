using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public enum IngredientType
    {
        Dough,
        Meat,
        Cheese,
        Sauce,
        Vegetable
    }

    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Owner = "Owner";
        public const string User = "User";

        public static string[] GetRoles()
        {
            return new string[]
            {
                Admin,
                Owner,
                User
            };
        }
    }

    public static class OrderStatus
    {
        public const string InProgress = "In Progress";
        public const string Accepted = "Accepted";
        public const string InDelivery = "Is being delivered";
        public const string Delivered = "Delivered";
        public const string Declined = "Order declined";

        public static string[] GetOrderStatuses()
        {
            return new string[]
            {
                InProgress,
                Accepted,
                InDelivery,
                Delivered,
                Declined
            };
        }
    }

    public static class PizzaSize
    {
        public const string Small = "Small";
        public const string Medium = "Medium";
        public const string Large = "Large";

        public static string[] getPizzaSizes()
        {
            return new string[]
            {
                Small,
                Medium,
                Large
            };
        }
    }
}