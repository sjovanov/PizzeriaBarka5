using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CartOrder
    {
        public Dictionary<Pizza, int> Items { get; set; }

        public CartOrder()
        {
            Items = new Dictionary<Pizza, int>();
        }
    }
}