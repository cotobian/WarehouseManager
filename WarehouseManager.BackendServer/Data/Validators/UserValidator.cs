using FluentValidation;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Data.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
        }
    }
}