﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models.Dtos
{
    public class PizzaDto
    {
        /// <summary>
        /// The Id of the pizza
        /// </summary>
        public Guid PizzaId { get; set; }
        /// <summary>
        /// The name of the pizza
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The user that created the pizza
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// List of ingredients in the pizza
        /// </summary>
        public List<IngredientDto> Ingredients { get; set; }

        /// <summary>
        /// The percentage of profit the pizzeria makes for selling a single pizza
        /// </summary>
        public double incomeCoeficient { get; set; }

        /// <summary>
        /// The price of the pizza
        /// </summary>
        /// <returns></returns>
        public double Price { get; set; }

        public string ImgUrl { get; set; }

        public string Size { get; set; }

        public PizzaDto()
        {
            Ingredients = new List<IngredientDto>();
        }

        public double getPizzaForMedium()
        {
            return 1.2 * Price * (1 + incomeCoeficient);
        }
        public double getPizzaForLarge()
        {
            return 1.4 * Price * (1 + incomeCoeficient);
        }
        public int getPriceInt(double price)
        {
            return Convert.ToInt32(price);
        }
        public double getPrice()
        {
            if (Size.Equals(PizzaSize.Large))
            {
                return getPizzaForLarge();
            }
            else if (Size.Equals(PizzaSize.Medium))
            {
                return getPizzaForMedium();
            }
            else
            {
                return Price * (1 + incomeCoeficient);
            }
        }
    }
}