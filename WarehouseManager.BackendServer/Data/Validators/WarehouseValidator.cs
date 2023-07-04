using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class WarehouseValidator : AbstractValidator<Warehouse>
    {
        public WarehouseValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Warehouse name cannot be null")
                .MaximumLength(50).WithMessage("Warehouse name cannot exceed 50 characters");
        }
    }
}