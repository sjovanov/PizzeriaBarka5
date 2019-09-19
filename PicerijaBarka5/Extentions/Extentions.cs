using PicerijaBarka5.Models;
using PicerijaBarka5.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Extentions
{
    public static class Extentions
    {
        public static IngredientDto toIngredientDto(this Ingredient dbIngredient)
        {
            return new IngredientDto
            {
                IngredientId = dbIngredient.IngredientId,
                Name = dbIngredient.Name,
                IngredientType = dbIngredient.IngredientType,
                Price = dbIngredient.getPriceForIngredientInSmallPizza(),
                QuantityPerSmallPizza = dbIngredient.QuantityPerSmallPizza
            };
        }

        public static PizzaDto toPizzaDto(this Pizza dbPizza)
        {
            return new PizzaDto
            {
                PizzaId = dbPizza.PizzaId,
                Name = dbPizza.Name,
                Ingredients = dbPizza.Ingredients.Select(ingredient => ingredient.toIngredientDto()).ToList(),
                UserFk = dbPizza.UserFk,
                Price = dbPizza.getPrice(),
                incomeCoeficient = dbPizza.IncomeCoeficient
            };
        }

        public static PizzaOrderDto toOrderDto(this PizzaOrder dbPizzaOrder)
        {
            return new PizzaOrderDto
            {
                OrderId = dbPizzaOrder.OrderId,
                Address = dbPizzaOrder.Address,
                Items = dbPizzaOrder.Items.Select(item => item.toPizzaDto()).ToList(),
                OrderStatus = dbPizzaOrder.OrderStatus,
                UserFk = dbPizzaOrder.UserFk
            };
        }
    }
}