using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PicerijaBarka5.Models
{
    public class PizzaOrderItem
    {
        [Key]
        public Guid PizzaOrderItemId { get; set; }

        [ForeignKey("Pizza")]
        public Guid PizzaId { get; set; }

        public Pizza Pizza { get; set; }

        public uint Quantity { get; set; }

        public virtual ICollection<PizzaOrder> Orders { get; set; }

        public PizzaOrderItem()
        {
            Orders = new List<PizzaOrder>();
        }

        public PizzaOrderItem(Pizza pizza, uint Quantity)
        {
            this.Pizza = pizza;
            this.PizzaId = pizza.PizzaId;
            this.Quantity = Quantity;
            Orders = new List<PizzaOrder>();
        }
    }
}