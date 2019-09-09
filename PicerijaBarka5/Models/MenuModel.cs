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
        public List<PizzaModel> Pizzas { get; set; }
        public List<IngredientModel> PizzeriaIngredients { get; set; }

        MenuModel()
        {
            Pizzas = new List<PizzaModel>();
            PizzeriaIngredients = new List<IngredientModel>();
        }
    }
}