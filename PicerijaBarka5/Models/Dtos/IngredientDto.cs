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
        /// The price per kilogram of the ingredient in the menu item
        /// </summary>
        [Required]
        public double Price { get; set; }

        public IngredientDto()
        {

        }
    }
}