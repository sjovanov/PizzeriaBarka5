using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class PizzaModel
    {
        /// <summary>
        /// The name of the pizza
        /// </summary>
        [Required]
        public String Name { get; set; }
        /// <summary>
        /// The price of the pizza. Price of pizza is sum of price multiplied by the quantity and the income coeficient.
        /// </summary>
        public double Price { get { return Ingredients.Sum(x => x.Price * x.QuantityPerSmallPizza/1000) * (1 + incomeCoeficient); } set { Price = value; } }
        /// <summary>
        /// List of ingredients in the pizza
        /// </summary>
        public List<IngredientModel> Ingredients { get; set; }
        private readonly double incomeCoeficient = 0.15;

        PizzaModel() {
            Ingredients = new List<IngredientModel>();
        }

    }
}