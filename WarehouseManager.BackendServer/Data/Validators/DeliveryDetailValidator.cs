using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class DeliveryDetailValidator : AbstractValidator<DeliveryDetail>
    {
        public DeliveryDetailValidator()
        {
            RuleFor(x => x.ReceiptDetailId).NotEmpty()
                .WithMessage("Receipt Detail cannot be null!");
            RuleFor(x => x.Quantity).NotEmpty()
                .WithMessage("Quantity cannot be null");
            RuleFor(x => x.FactoryId).NotEmpty()
                .WithMessage("Factory cannot be null");
            RuleFor(x => x.PickUpPosition).NotEmpty()
                .WithMessage("Position cannot be null");
            RuleFor(x => x.Note).MaximumLength(200)
                .WithMessage("Note length cannot exceed 200 charactors");
        }
    }
}