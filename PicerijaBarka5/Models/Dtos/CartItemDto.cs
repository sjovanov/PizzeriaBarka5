using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models.Dtos
{
    public class CartItemDto
    {
        /// <summary>
        /// Navigational property to Pizza
        /// </summary>
        public PizzaDto Pizza { get; set; }

        /// <summary>
        /// Quantity of item to order
        /// </summary>
        public int Quantity { get; set; }

        public string PizzaSize { get; set; }

        public CartItemDto()
        {

        }
    }
}