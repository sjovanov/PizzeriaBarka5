using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public static class IngredientTypeEnum
    {
        public enum IngredientType
        {
            Dough,
            Meat,
            Cheese,
            Sauce,
            Vegetable
        }
        public enum Roles
        {
            Admin,
            User,
            Owner
        }
    }
}