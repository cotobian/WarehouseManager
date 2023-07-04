using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class RolePermissionValidator : AbstractValidator<RolePermission>
    {
        public RolePermissionValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role cannot be null");
            RuleFor(x => x.FunctionId).NotEmpty().WithMessage("Function cannot be null");
            RuleFor(x => x.Command).NotEmpty().WithMessage("Command cannot be null");
        }
    }
}