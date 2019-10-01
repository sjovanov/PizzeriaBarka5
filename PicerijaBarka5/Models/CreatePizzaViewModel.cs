using PicerijaBarka5.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicerijaBarka5.Models
{
    public class CreatePizzaViewModel
    {
        public Guid? PizzaId { get; set; }

        [Required]
        public string Name { set; get; }

        [Display(Name = "Income coeficient")]
        public double IncomeCoef { set; get; }

        public string ImgUrl { set; get; }

        [Required(ErrorMessage = "You have to select at least one ingredient")]
        public ICollection<string> selectedIngredients { set; get; }

        public Dictionary<string, ICollection<IngredientDto>> TypeIngredientListPairs { set; get; }

        public string Size { set; get; }

        public CreatePizzaViewModel()
        {
            selectedIngredients = new List<string>();
            TypeIngredientListPairs = new Dictionary<string, ICollection<IngredientDto>>();
        }
    }
}
