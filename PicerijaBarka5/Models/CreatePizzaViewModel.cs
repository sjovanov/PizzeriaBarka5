using PicerijaBarka5.Models.Dtos;
using PicerijaBarka5.Services;
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

        [IngredientValidator(ErrorMessage = "Please select at least one ingredient of type dough")]
        public ICollection<string> selectedIngredients { set; get; }

        public Dictionary<string, ICollection<IngredientDto>> TypeIngredientListPairs { set; get; }

        public string Size { set; get; }

        public CreatePizzaViewModel()
        {
            selectedIngredients = new List<string>();
            TypeIngredientListPairs = new Dictionary<string, ICollection<IngredientDto>>();
        }

        public class IngredientValidator : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var ing = (ICollection<string>)value;
                if (ing == null || ing.Count == 0) { return false; }
                Repository rep = Repository.GetInstance();

                return ing.Select(x => rep.GetIngredient(Guid.Parse(x))).ToList().Any(y => y.IngredientType == IngredientType.Dough);
            }
        }
    }

}
