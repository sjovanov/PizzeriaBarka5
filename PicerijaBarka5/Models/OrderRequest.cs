using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class OrderRequest
    {
        public Dictionary<Guid, int> Items { get; set; }

        public OrderRequest()
        {
            Items = new Dictionary<Guid, int>();
        }
    }
}