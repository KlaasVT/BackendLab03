using System.Data;
namespace Sneakers.API.Validators;
public class SneakerValidator : AbstractValidator<Sneaker>
{
    public SneakerValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name cannot exceed 50 characters");
        RuleFor(s => s.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
        RuleFor(s => s.Occasions)
            .NotEmpty()
            .WithMessage("At least 1 occasion is Required");
        RuleFor(s => s.Brand)
            .NotEmpty()
            .WithMessage("Brand is required");
    }
}