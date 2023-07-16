using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Customer name cannot be null")
                .MaximumLength(200).WithMessage("Customer name length cannot exceed 200 characters");
            RuleFor(x => x.Address).MaximumLength(500)
                .WithMessage("Customer address length cannot exceeed 500 characters");
            RuleFor(x => x.PhoneNumber).MaximumLength(50)
                .WithMessage("Phone number length cannot exceed 50 characters");
            RuleFor(x => x.Email).MaximumLength(50)
                .WithMessage("Email length cannot exceed 50 characters");
            RuleFor(x => x.Website).MaximumLength(50)
                .WithMessage("Website length cannot exceed 50 characters");
            RuleFor(x => x.Note).MaximumLength(200)
                .WithMessage("Note length cannot exceed 50 characters");
        }
    }
}