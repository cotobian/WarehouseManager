using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class DeliveryOrderValidator : AbstractValidator<DeliveryOrder>
    {
        public DeliveryOrderValidator()
        {
            RuleFor(x => x.OrderNo).NotEmpty()
                .WithMessage("Order number cannot be null");
            RuleFor(x => x.ExportDate).NotEmpty()
                .WithMessage("Export date cannot be null");
            RuleFor(x => x.DelivererName).MaximumLength(50)
                .WithMessage("Deliverer name length cannot exceed 50 characters");
            RuleFor(x => x.OriginDO).MaximumLength(50)
                .WithMessage("OriginDO length cannot exceed 50 characters");
            RuleFor(x => x.TruckNo).MaximumLength(20)
                .WithMessage("Truck Number length cannot exceed 20 characters");
            RuleFor(x => x.CntrNo).MaximumLength(20)
                .WithMessage("Container Number length cannot exceed 20 characters");
            RuleFor(x => x.SealNo).MaximumLength(20)
                .WithMessage("Seal Number length cannot exceed 20 characters");
            RuleFor(x => x.Inv).MaximumLength(20)
                .WithMessage("Inv length cannot exceed 20 characters");
            RuleFor(x => x.PhoneNumber).MaximumLength(20)
                .WithMessage("Phone Number length cannot exceed 20 characters");
        }
    }
}