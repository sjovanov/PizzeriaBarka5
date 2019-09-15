﻿using System;
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

        public virtual ICollection<Pizza> PizzasToOrder { get; set; }

        [Required]
        public string Address { get; set; }

        public PizzaOrder()
        {
            PizzasToOrder = new List<Pizza>();
        }
    }
}