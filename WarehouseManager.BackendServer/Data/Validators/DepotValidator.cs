using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class DepotValidator : AbstractValidator<Depot>
    {
        public DepotValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Depot name cannot be null")
                .MaximumLength(200).WithMessage("Depot name length cannot exceed 200 characters");
            RuleFor(x => x.DepotCode).NotEmpty().WithMessage("Depot code cannot be null")
                .MaximumLength(20).WithMessage("Depot code length cannot exceed 20 characters");
        }
    }
}