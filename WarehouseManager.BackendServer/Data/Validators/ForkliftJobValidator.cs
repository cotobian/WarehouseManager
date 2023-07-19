using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class ForkliftJobValidator : AbstractValidator<ForkliftJob>
    {
        public ForkliftJobValidator()
        {
            RuleFor(x => x.PalledId).NotEmpty().WithMessage("Pallet cannot be null");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity cannot be null");
        }
    }
}