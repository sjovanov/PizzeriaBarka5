using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CreatePizzaViewModel
    {
        [Required]
        public string Name { set; get; }
        public double IncomeCoef { set; get; }
        [Required(ErrorMessage = "Dough type is required")]
        public string Dough { set; get; }
        [Required(ErrorMessage = "You have to select at least one ingredient")]
        public ICollection<string> selectedIngredients { set; get; }
        public ICollection<Ingredient> availableIngredients { set; get; }

        public CreatePizzaViewModel()
        {
            selectedIngredients = new List<string>();
            availableIngredients = new List<Ingredient>();
        }
    }
}