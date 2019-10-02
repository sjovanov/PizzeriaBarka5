using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models.Dtos
{
    public class PizzaOrderDto
    {
        public Guid OrderId { get; set; }

        public UserDto User { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public DateTime TimeOfOrder { get; set; }

        public List<CartItemDto> Items { get; set; }

        public int Rating { get; set; }

        public PizzaOrderDto()
        {
            Items = new List<CartItemDto>();
            Rating = 0;
        }
    }
}