using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                QuantityPerSmallPizza = dbIngredient.QuantityPerSmallPizza,
                QuantityInStock = dbIngredient.QuantityInStock
            };
        }

        public static UserDto toUserDto(this ApplicationUser dbUser)
        {
            return new UserDto
            {
                UserName = dbUser.UserName,
                Email = dbUser.Email
            };
        } 

        public static PizzaDto toPizzaDto(this Pizza dbPizza)
        {
            return new PizzaDto
            {
                PizzaId = dbPizza.PizzaId,
                Name = dbPizza.Name,
                Ingredients = dbPizza.Ingredients
                                        .Select(ingredient => ingredient.toIngredientDto())
                                        .ToList(),
                Price = dbPizza.getPrice(),
                User = dbPizza.User.toUserDto(),
                incomeCoeficient = dbPizza.IncomeCoeficient,
                ImgUrl = dbPizza.ImgUrl
            };
        }

        public static CartItemDto toCartItemDto(this CartItem cartItem)
        {
            return new CartItemDto
            {
                Pizza = cartItem.Pizza.toPizzaDto(),
                Quantity = cartItem.Quantity
            };
        }

        public static PizzaOrderDto toOrderDto(this PizzaOrder dbPizzaOrder)
        {
            return new PizzaOrderDto
            {
                User = dbPizzaOrder.User.toUserDto(),
                OrderId = dbPizzaOrder.OrderId,
                Address = dbPizzaOrder.Address,
                Items = dbPizzaOrder.Items.Select(item => item.toCartItemDto())
                                            .ToList(),
                Status = dbPizzaOrder.Status,
                TimeOfOrder = dbPizzaOrder.TimeOfOrder
            };
        }

    }
}