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
        public string Status { get; set; }

        [Required]
        public DateTime TimeOfOrder { get; set; }

        public virtual ICollection<CartItem> Items { get; set; }

        public int Rating { get; set; }

        public PizzaOrder()
        {
            Status = OrderStatus.InProgress;
            Items = new List<CartItem>();
            Rating = 0;
        }
    }
}