using FluentValidation;
using PopsicleFactory.Application.Dtos;

namespace PopsicleFactory.Application.Validators
{
    public class CreatePopsicleDtoValidator : AbstractValidator<CreatePopsicleDto>
    {
        public CreatePopsicleDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required in the payload")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(p => p.Flavor)
                .NotEmpty().WithMessage("Flavor is required at this time ")
                .MaximumLength(50).WithMessage("Flavor must not exceed 50 characters please");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");
        }
    }
}
