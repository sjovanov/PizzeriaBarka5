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

    public enum OrderStatus
    {
        InProgress,
        Accepted,
        InDelivery,
        Delivered
    }

}