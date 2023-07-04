using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class PortValidator : AbstractValidator<Port>
    {
        public PortValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Port name cannot be null")
                .MaximumLength(200).WithMessage("Port name cannot exceed 200 characters");
            RuleFor(x => x.PortCode).NotEmpty().WithMessage("Port code cannot be null")
                .MaximumLength(20).WithMessage("Port code cannot exceed 20 characters");
        }
    }
}