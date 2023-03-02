namespace Lab03_Cars.Validators;
public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        RuleFor(car => car.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(car => car.Brand).NotNull().WithMessage("Brand is required");
    }
}
