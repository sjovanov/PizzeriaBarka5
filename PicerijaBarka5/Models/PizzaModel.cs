﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class Pizza
    {
        [Key]
        public Guid PizzaId { get; set; }

        /// <summary>
        /// The name of the pizza
        /// </summary>
        [Required]
        public String Name { get; set; }

        /// <summary>
        /// The price of the pizza. Price of pizza is sum of price multiplied by the quantity and the income coeficient.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// List of ingredients in the pizza
        /// </summary>
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        
        /// <summary>
        /// The percentage of profit the pizzeria makes for selling a single pizza
        /// </summary>
        public double incomeCoeficient { get; set; }

        /// <summary>
        /// Returns the price for making a small pizza based on the price of its ingredients and the incomeCoeficient of the pizzeria
        /// </summary>
        /// <returns></returns>
        public double getPrice()
        {
            if (Ingredients.Count > 0)
            {
                return Ingredients.Sum(it => it.getPriceForIngredientInSmallPizza()) * (1 + incomeCoeficient);
            }
            else
            {
                return 0;
            }
        }

        public Pizza()
        {
            Ingredients = new List<Ingredient>();
        }

        public Pizza(Guid id, string name, ICollection<Ingredient> ingredients, double incomeCoef = 0)
        {
            PizzaId = id;
            Name = name;
            Ingredients = ingredients;
            incomeCoeficient = incomeCoef;
        }

    }
}