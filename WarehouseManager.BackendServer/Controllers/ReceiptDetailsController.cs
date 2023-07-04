using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    public class ReceiptDetailsController : BaseController<ReceiptDetail>
    {
        public ReceiptDetailsController(WhContext context) : base(context)
        {
        }
    }
}