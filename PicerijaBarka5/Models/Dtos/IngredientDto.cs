using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models.Dtos
{
    public class IngredientDto
    {
        /// <summary>
        /// The Id of the ingredient
        /// </summary>
        public Guid IngredientId { get; set; }

        /// <summary>
        /// The name of the ingredient in the menu item
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The quantity of the ingredient used when making a small pizza in the menu in grams
        /// </summary>
        [Display(Name = "Quantity per small pizza")]
        public int QuantityPerSmallPizza { get; set; }

        ///<summary>
        /// The type of ingredient
        /// </summary>
        public IngredientType IngredientType { get; set; }

        /// <summary>
        /// List of pizzas where the ingredient
        /// </summary>
        public List<PizzaDto> Pizzas { get; set; }

        /// <summary>
        /// The price per kilogram of the ingredient in the menu item
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Quantity of the ingredient in stock
        /// </summary>
        [Display(Name = "Quantity of ingredient in stock")]
        public double QuantityInStock { get; set; }

        /// <summary>
        /// Returns the price for the ingredient to make a single small pizza
        /// </summary>
        /// <returns></returns>
        public double getPriceForIngredientInSmallPizza()
        {
            return QuantityPerSmallPizza * Price / 1000;
        }

        public IngredientDto()
        {
            Pizzas = new List<PizzaDto>();
        }
    }
}