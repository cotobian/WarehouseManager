using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class CurrentPositionValidator : AbstractValidator<CurrentPosition>
    {
        public CurrentPositionValidator()
        {
            RuleFor(x => x.PositionId).NotEmpty().WithMessage("Position cannot be null");
        }
    }
}