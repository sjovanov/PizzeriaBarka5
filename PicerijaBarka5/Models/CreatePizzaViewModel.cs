using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CreatePizzaViewModel
    {
        public string Name { set; get; }
        public double IncomeCoef { set; get; }
        public ICollection<String> selectedIngredients { set; get; }
        public ICollection<Ingredient> availableIngredients { set; get; }
    }
}