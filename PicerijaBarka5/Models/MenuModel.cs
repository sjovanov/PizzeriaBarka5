using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class MenuModel
    {
        /// <summary>
        /// The pizzas that are on the menu
        /// </summary>
        public List<Pizza> Pizzas { get; set; }
        public List<Ingredient> PizzeriaIngredients { get; set; }

        MenuModel()
        {
            Pizzas = new List<Pizza>();
            PizzeriaIngredients = new List<Ingredient>();
        }
    }
}