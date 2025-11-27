using FluentValidation;
using FoodieHub.API.Entities;

namespace FoodieHub.API.ValidationRules
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required.")
                                       .MaximumLength(60).WithMessage("Product name cannot exceed 60 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.").LessThan(1000).WithMessage("Price must be less than 1,000.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image URL is required.");


        }
    }
}
