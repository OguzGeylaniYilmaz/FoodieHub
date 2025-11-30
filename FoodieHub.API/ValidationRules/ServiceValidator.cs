using FluentValidation;
using FoodieHub.API.Entities;

namespace FoodieHub.API.ValidationRules
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Service title is required.")
                                       .MaximumLength(80).WithMessage("Service Title cannot exceed 80 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            //RuleFor(x => x.IconUrl).NotEmpty().WithMessage("Image URL is required.");
        }
    }
}
