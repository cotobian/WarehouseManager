using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Constants;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class AgentsController : BaseController<Agent>
    {
        #region Init

        public AgentsController(WhContext context) : base(context)
        {
        }

        #endregion Init
    }
}