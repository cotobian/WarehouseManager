﻿using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class PalletValidator : AbstractValidator<Pallet>
    {
        public PalletValidator()
        {
            RuleFor(x => x.PalletNo).NotEmpty().WithMessage("Pallet number cannot be null")
                .MaximumLength(50).WithMessage("Pallet number cannot exceed 50 characters");
            RuleFor(x => x.ReceiptDetailId).NotEmpty().WithMessage("Receipt Detail cannot be null");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantiy cannot be null");
        }
    }
}