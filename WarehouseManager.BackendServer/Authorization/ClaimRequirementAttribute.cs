using Microsoft.AspNetCore.Mvc;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Authorization
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(Command command) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { command };
        }
    }
}