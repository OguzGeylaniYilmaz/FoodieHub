using FluentValidation;
using FoodieHub.API.Entities;

namespace FoodieHub.API.ValidationRules
{
    public class TestimonialValidator : AbstractValidator<Testimonial>
    {
        public TestimonialValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Fullname is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is required.");
            RuleFor(x => x.Comment).MinimumLength(10).WithMessage("Comment must be at least 10 characters long.");
            RuleFor(x => x.Comment).MaximumLength(300).WithMessage("Comment must be maximum 300 characters long.");

        }
    }
}
