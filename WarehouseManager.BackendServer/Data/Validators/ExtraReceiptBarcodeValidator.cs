using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace HTM.BackEnds.Data.Validators
{
    public class ExtraReceiptBarcodeValidator : AbstractValidator<ExtraReceiptBarcode>
    {
        public ExtraReceiptBarcodeValidator()
        {
            RuleFor(x => x.ReceiptDetailId).NotEmpty().WithMessage("Receipt Detail cannot be null");
            RuleFor(x => x.Data).NotEmpty().WithMessage("Data cannot be null");
        }
    }
}