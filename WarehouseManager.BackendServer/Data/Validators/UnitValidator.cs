using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class UnitValidator : AbstractValidator<Unit>
    {
        public UnitValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name value cannot be null!")
                .MaximumLength(200).WithMessage("Name length cannot exceed 200 characters");
        }
    }
}