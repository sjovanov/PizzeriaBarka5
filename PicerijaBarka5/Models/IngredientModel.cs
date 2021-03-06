﻿using System;
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
        
        ///<summary>
        /// The type of ingredient
        /// </summary>
        [Required]
        public IngredientType IngredientType { get; set; }

        /// <summary>
        /// The price per kilogram of the ingredient in the menu item
        /// </summary>
        [Required]
        public double Price { get; set; }

        ///<summary>
        /// Navigational property to pizzas
        ///</summary>
        public virtual ICollection<Pizza> Pizzas { get; set; }

        /// <summary>
        /// Quantity of the ingredient in stock
        /// </summary>
        public double QuantityInStock { get; set; }

        public Ingredient()
        {
            Pizzas = new List<Pizza>();
        }
    }
}