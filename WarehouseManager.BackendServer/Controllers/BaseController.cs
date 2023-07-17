using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
        public readonly IConfiguration _configuration;

        public BaseController(WhContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()

        {
            return Ok(await _context.Set<T>().ToListAsync());
        }

        //[HttpPost("predicate")]
        //public async Task<IActionResult> GetPredicate(Expression<Func<T, bool>> predicate)
        //{
        //    return Ok(await _context.Set<T>().Where(predicate).ToListAsync());
        //}

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Set<T>().FindAsync(id));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T item)
        {
            string typeName = typeof(T).Name;
            string validatorTypeName = typeName + "Validator";
            var validatorType = typeof(Program).Assembly.GetTypes().FirstOrDefault(t => t.Name == validatorTypeName);
            if (validatorType != null)
            {
                var validator = Activator.CreateInstance(validatorType);
                var validateMethod = validatorType.GetMethod("Validate", new[] { typeof(T) });
                var results = (ValidationResult)validateMethod.Invoke(validator, new[] { item });

                if (results.IsValid)
                {
                    _context.Set<T>().Add(item);
                    await _context.SaveChangesAsync();
                    return Ok(item);
                }
                else return BadRequest($"Property '{results.Errors[0].PropertyName}': {results.Errors[0].ErrorMessage}");
            }
            else return BadRequest("Validator not found");
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(T item)
        {
            PropertyInfo idProperty = typeof(T).GetProperty("Id");
            int? id = (int?)idProperty.GetValue(item);
            string typeName = typeof(T).Name;
            string validatorTypeName = typeName + "Validator";
            var validatorType = typeof(Program).Assembly.GetTypes().FirstOrDefault(t => t.Name == validatorTypeName);
            if (validatorType != null)
            {
                var validator = Activator.CreateInstance(validatorType);
                var validateMethod = validatorType.GetMethod("Validate", new[] { typeof(T) });
                var results = (ValidationResult)validateMethod.Invoke(validator, new[] { item });

                if (results.IsValid)
                {
                    T res = await _context.Set<T>().FindAsync(id);
                    if (res != null)
                    {
                        _context.Entry(res).CurrentValues.SetValues(item);

                        await _context.SaveChangesAsync();
                        return Ok(res);
                    }
                    else return NotFound();
                }
                else return BadRequest($"Property '{results.Errors[0].PropertyName}': {results.Errors[0].ErrorMessage}");
            }
            else return BadRequest("Validator not found");
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
                    return Ok();
                }
                else return BadRequest("No Status field");
            }
            else return BadRequest("No record found");
        }
    }
}