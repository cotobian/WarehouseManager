using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class ReceiptOrderValidator : AbstractValidator<ReceiptOrder>
    {
        public ReceiptOrderValidator()
        {
            RuleFor(x => x.OrderNo).NotEmpty().WithMessage("Order No cannot be null")
                .MaximumLength(30).WithMessage("Order No length cannot exceed 30 characters");
            RuleFor(x => x.TruckNo).MaximumLength(20)
                .WithMessage("TruckNo length cannot exceed 20 characters");
        }
    }
}