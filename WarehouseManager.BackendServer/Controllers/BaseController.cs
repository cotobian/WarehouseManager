using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WarehouseManager.BackendServer.Data;

namespace WarehouseManager.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class BaseController<T> : ControllerBase where T : class
    {
        public readonly WhContext _context;

        public BaseController(WhContext context)
        {
            _context = context;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Set<T>().ToListAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Set<T>().FindAsync(id));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T item)
        {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(int id, T item)
        {
            T res = await _context.Set<T>().FindAsync(id);
            if (res != null)
            {
                res = item;
                await _context.SaveChangesAsync();
                return Ok(res);
            }
            else return NotFound();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            T res = await _context.Set<T>().FindAsync(id);
            if (res != null)
            {
                PropertyInfo statusProperty = res.GetType().GetProperty("Status");
                if (statusProperty != null && statusProperty.PropertyType == typeof(bool))
                {
                    statusProperty.SetValue(res, false);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else return BadRequest("No Status field");
            }
            else return NotFound();
        }
    }
}