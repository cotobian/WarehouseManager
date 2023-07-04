using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class ExtraReceiptDetailValidator : AbstractValidator<ExtraReceiptDetail>
    {
        public ExtraReceiptDetailValidator()
        {
            RuleFor(x => x.ReceiptDetailId).NotEmpty().WithMessage("Receipt Detail cannot be null");
            RuleFor(x => x.Data).NotEmpty().WithMessage("Data cannot be null");
        }
    }
}