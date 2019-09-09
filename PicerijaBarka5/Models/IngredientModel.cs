using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class IngredientModel
    {
        /// <summary>
        /// The name of the ingredient in the menu item
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The quantity of the ingredient used when making a small pizza in the menu
        /// </summary>
        [Required]
        public int QuantityPerSmallPizza { get; set; }
        /// <summary>
        /// The price per kilogram of the ingredient in the menu item
        /// </summary>
        [Required]
        public double Price { get; set; }
    }
}