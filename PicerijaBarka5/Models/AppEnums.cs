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
        public static readonly string Admin = "Admin";
        public static readonly string Owner = "Owner";
        public static readonly string User = "User";

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
        public static readonly string InProgress = "In Progress";
        public static readonly string Accepted = "Accepted";
        public static readonly string InDelivery = "Is being delivered";
        public static readonly string Delivered = "Delivered";
        public static readonly string Declined = "Order declined";

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

}