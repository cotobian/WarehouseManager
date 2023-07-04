using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class WarehousePositionValidator : AbstractValidator<WarehousePosition>
    {
        public WarehousePositionValidator()
        {
            RuleFor(x => x.WarehouseId).NotEmpty().WithMessage("Warehouse cannot be null");
            RuleFor(x => x.Bay).NotEmpty().WithMessage("Bay cannot be null")
                .MaximumLength(10).WithMessage("Bay length cannot exceed 10 characters");
            RuleFor(x => x.Row).NotEmpty().WithMessage("Row cannot be null")
                .MaximumLength(10).WithMessage("Row length cannot exceed 10 characters");
            RuleFor(x => x.Tier).NotEmpty().WithMessage("Tier cannot be null")
                .MaximumLength(10).WithMessage("Tier length cannot exceed 10 characters");
        }
    }
}