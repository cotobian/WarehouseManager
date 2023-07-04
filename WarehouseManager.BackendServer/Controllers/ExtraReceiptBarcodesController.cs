using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;

namespace WarehouseManager.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraReceiptBarcodesController : BaseController<ExtraReceiptBarcode>
    {
        public ExtraReceiptBarcodesController(WhContext context) : base(context)
        {
        }
    }
}