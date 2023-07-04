using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class FactoryValidator : AbstractValidator<Factory>
    {
        public FactoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Factory name cannot be null")
                .MaximumLength(200).WithMessage("Factory name length cannot exceed 200 characters");
            RuleFor(x => x.Address).MaximumLength(500)
                .WithMessage("Factory Address length cannot exceed 500 characters");
        }
    }
}