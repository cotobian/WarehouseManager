using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class FunctionValidator : AbstractValidator<Function>
    {
        public FunctionValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Function name cannot be null")
                .MaximumLength(200).WithMessage("Function name length cannot exceed 200 characters");
            RuleFor(x => x.Url).MaximumLength(200).WithMessage("Url length cannot exceed 200 characters");
        }
    }
}