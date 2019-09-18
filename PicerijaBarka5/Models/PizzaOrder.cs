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

        [ForeignKey("User")]
        public string UserFk { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<PizzaOrderItem> Items { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }

        public PizzaOrder()
        {
            OrderStatus = OrderStatus.InProgress;
            Items = new List<PizzaOrderItem>();
        }
    }
}