using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Authorization
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        public Command _command { get; }
        public readonly WhContext _context;

        public ClaimRequirementFilter(Command command, WhContext context)
        {
            _command = command;
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            var controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
            var controllerType = controllerActionDescriptor?.ControllerTypeInfo.AsType();

            var permissionClaim = context.HttpContext.User.Claims
                .SingleOrDefault(c => c.Type == SystemConstants.Permissions);
            if (permissionClaim != null)
            {
                var permissions = JsonConvert.DeserializeObject<List<string>>(permissionClaim.Value);
                if (!permissions.Contains(GetFunctionId(controllerType) + "_" + _command))
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }

        private int? GetFunctionId(Type controllerType)
        {
            int? functionId = _context.Functions.Where(c => c.Controller.Equals(controllerType.Name)).Select(c => c.Id).FirstOrDefault();
            return functionId;
        }
    }
}