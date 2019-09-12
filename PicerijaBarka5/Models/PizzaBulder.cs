using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class PizzaBulder
    {
        public Guid pizzaId;
        public string name;
        public double incomeCoef;
        public List<Ingredient> Ingredients = new List<Ingredient>();

        public PizzaBulder() { }

        public PizzaBulder withIncomeCoef(double incomeCoef)
        {
            this.incomeCoef = incomeCoef;
            return this;
        }

        public PizzaBulder withName(string name)
        {
            this.name = name;
            return this;
        }

        public PizzaBulder withIngredients(List<Ingredient> ingredients)
        {
            Ingredients.AddRange(ingredients);
            return this;
        }

        public Pizza build()
        {
            var ingredientsToAdd = new List<Ingredient>();
            ingredientsToAdd.AddRange(Ingredients);
            pizzaId = Guid.NewGuid();
            return new Pizza(pizzaId, name, ingredientsToAdd, incomeCoef);
        }
    }


}
