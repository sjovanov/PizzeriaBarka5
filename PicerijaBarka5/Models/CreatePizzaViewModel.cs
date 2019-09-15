using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CreatePizzaViewModel
    {
        public string Name { set; get; }
        public string UserEmail { get; set; }
        public double IncomeCoef { set; get; }
        [Required(ErrorMessage = "Dough type is required")]
        public string Dough { set; get; }
        [Required(ErrorMessage = "You have to select at least one ingredient")]
        public ICollection<String> selectedIngredients { set; get; }
        public ICollection<Ingredient> availableIngredients { set; get; }
    }
}