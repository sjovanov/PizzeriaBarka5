using PicerijaBarka5.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }

        /// <summary>
        /// Navigational property to Pizza
        /// </summary>
        public virtual Pizza Pizza { get; set; }

        /// <summary>
        /// Navigational property to PizzaOrder
        /// </summary>
        public virtual PizzaOrder PizzaOrder { get; set; }

        /// <summary>
        /// Quantity of item to order
        /// </summary>
        public int Quantity { get; set; }

        public CartItem()
        {

        }
    }
}