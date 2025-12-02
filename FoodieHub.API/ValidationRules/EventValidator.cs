using FluentValidation;
using FoodieHub.API.Entities;

namespace FoodieHub.API.ValidationRules
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
