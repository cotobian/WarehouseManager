using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class PalletDetailValidator : AbstractValidator<PalletDetail>
    {
        public PalletDetailValidator()
        {
            RuleFor(x => x.PalletId).NotEmpty()
                .WithMessage("PalletId cannot be null");
            RuleFor(x => x.ReceiptDetailId).NotEmpty()
                .WithMessage("ReceiptDetailId cannot be null");
            RuleFor(x => x.Quantity).NotEmpty()
                .WithMessage("Quantity cannot be null");
        }
    }
}