using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CreatePizzaViewModel
    {
        public string Name;
        public double IncomeCoef;
        public ICollection<String> selectedIngredients;
        public ICollection<Ingredient> availableIngredients;
    }
}