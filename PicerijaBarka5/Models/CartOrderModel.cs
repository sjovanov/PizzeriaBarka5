using PicerijaBarka5.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CartOrderDto
    {
        public List<CartItemDto> Items { get; set; }

        public CartOrderDto()
        {
            Items = new List<CartItemDto>();
        }
    }

    public class PizzaDtoDictionaryComparer : IEqualityComparer<PizzaDto>
    {
        public bool Equals(PizzaDto x, PizzaDto y)
        {
            return x.PizzaId == y.PizzaId;
        }

        public int GetHashCode(PizzaDto obj)
        {
            return obj.PizzaId.GetHashCode();
        }
    }
}