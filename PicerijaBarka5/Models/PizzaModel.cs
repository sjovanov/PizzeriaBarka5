using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// The user that created the pizza
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// List of ingredients in the pizza
        /// </summary>
        public virtual ICollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// List of orders the pizza has been placed in
        /// </summary>
        public virtual ICollection<PizzaOrder> Orders { get; set; }

        /// <summary>
        /// Navigational property to items in cart
        /// </summary>
        public virtual ICollection<CartItem> CartItems { get; set; }

        /// <summary>
        /// The percentage of profit the pizzeria makes for selling a single pizza
        /// </summary>
        public double IncomeCoeficient { get; set; }

        /// <summary>
        /// Url to the pizza image
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// Returns the price of the pizza
        /// </summary>
        /// <returns></returns>
        public double Price { get; set; }

        public string Size { get; set; }

        public Pizza()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<PizzaOrder>();
        }

        public Pizza(Guid id, string name, ICollection<Ingredient> ingredients, string imgUrl, string size, double incomeCoef = 0)
        {
            PizzaId = id;
            Name = name;
            Ingredients = ingredients;
            IncomeCoeficient = incomeCoef;
            Orders = new List<PizzaOrder>();
            Size = size;
            ImgUrl = imgUrl;
        }
    }
}