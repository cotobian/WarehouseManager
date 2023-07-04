using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class AgentValidator : AbstractValidator<Agent>
    {
        public AgentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name value cannot be null!")
                .MaximumLength(200).WithMessage("Name length cannot exceed 200 characters");
            RuleFor(x => x.ShortName).NotEmpty().WithMessage("Short name value cannot be null!")
                .MaximumLength(20).WithMessage("Name length cannot exceed 20 characters");
        }
    }
}