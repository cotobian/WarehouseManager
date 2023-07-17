using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class ReceiptDetailValidator : AbstractValidator<ReceiptDetail>
    {
        public ReceiptDetailValidator()
        {
        }
    }
}