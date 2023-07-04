using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class UserPermissionValidator : AbstractValidator<UserPermission>
    {
        public UserPermissionValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User cannot be null");
            RuleFor(x => x.FunctionId).NotEmpty().WithMessage("Function cannot be null");
            RuleFor(x => x.Command).NotEmpty().WithMessage("Command cannot be null");
        }
    }
}