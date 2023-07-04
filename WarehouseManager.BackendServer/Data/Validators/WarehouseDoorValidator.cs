using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class WarehouseDoorValidator : AbstractValidator<WarehouseDoor>
    {
        public WarehouseDoorValidator()
        {
            RuleFor(x => x.WarehouseId).NotEmpty().WithMessage("Warehouse cannot be null");
            RuleFor(x => x.DoorNo).NotEmpty().WithMessage("Warehouse door cannot be null")
                .MaximumLength(10).WithMessage("Warehouse door length cannot exceed 10 characters");
        }
    }
}