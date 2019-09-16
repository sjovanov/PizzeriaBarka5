using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PicerijaBarka5.Models
{
    public class PizzaOrderItem
    {
        [Key, ForeignKey("Pizza")]
        public Guid PizzaId { get; set; }

        public Pizza Pizza { get; set; }

        public uint Quantity { get; set; }

        public PizzaOrderItem(Pizza pizza, uint Quantity)
        {
            this.Pizza = pizza;
            this.Quantity = Quantity;
        }
    }
}