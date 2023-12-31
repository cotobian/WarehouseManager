﻿using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class RoleController : BaseController<Role>
    {
        public RoleController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}