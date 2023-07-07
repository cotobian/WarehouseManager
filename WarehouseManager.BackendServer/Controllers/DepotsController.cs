﻿using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class DepotsController : BaseController<Depot>
    {
        public DepotsController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}