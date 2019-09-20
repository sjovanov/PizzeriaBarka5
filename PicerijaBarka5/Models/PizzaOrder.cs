using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class PizzaOrder
    {
        [Key]
        public Guid OrderId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public virtual ICollection<Pizza> Items { get; set; }

        public PizzaOrder()
        {
            OrderStatus = OrderStatus.InProgress;
            Items = new List<Pizza>();
        }
    }
}