using FluentValidation;
using FoodieHub.API.Entities;

namespace FoodieHub.API.ValidationRules
{
    public class ChefValidator : AbstractValidator<Chef>
    {
        public ChefValidator()
        {
            RuleFor(c => c.NameSurname).NotEmpty().WithMessage("Chef name cannot be empty.");


        }
    }
}
