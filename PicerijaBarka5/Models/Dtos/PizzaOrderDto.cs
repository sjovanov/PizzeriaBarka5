using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models.Dtos
{
    public class PizzaOrderDto
    {
        public Guid OrderId { get; set; }

        public string UserFk { get; set; }

        public ApplicationUser User { get; set; }

        public string Address { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public List<PizzaDto> Items { get; set; }

        public PizzaOrderDto()
        {
            Items = new List<PizzaDto>();
        }
    }
}