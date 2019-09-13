using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class Ingredient
    {
        [Key]
        public Guid IngredientId { get; set; }
        /// <summary>
        /// The name of the ingredient in the menu item
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The quantity of the ingredient used when making a small pizza in the menu in grams
        /// </summary>
        [Required]
        [Display(Name = "Quantity per small pizza")]
        public int QuantityPerSmallPizza { get; set; }
        /// <summary>
        /// The price per kilogram of the ingredient in the menu item
        /// </summary>
        
        ///<summary>
        /// The type of ingredient
        /// </summary>
        [Required]
        public IngredientTypeEnum.IngredientType IngredientType { get; set; }

        [Required]
        public double Price { get; set; }
        ///<summary>
        /// Collection of pizzas that include the ingredient
        ///</summary>
        public virtual ICollection<Pizza> Pizzas { get; set; }

        public Ingredient()
        {
            Pizzas = new List<Pizza>();
        }

        /// <summary>
        /// Returns the price for the ingredient to make a single small pizza
        /// </summary>
        /// <returns></returns>
        public double getPriceForIngredientInSmallPizza()
        {
            return QuantityPerSmallPizza * Price / 1000; 
        }

    }
}