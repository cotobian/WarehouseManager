using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.BackendServer.Data.Validators;
using WarehouseManager.ViewModels.Admin.User;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    public class UserController : BaseController<User>
    {
        public UserController(WhContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        #region Private Methods

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion Private Methods

        #region Public Methods

        [HttpGet]
        public override async Task<IActionResult> GetAll()
        {
            var query = from u in _context.Users
                        join d in _context.Departments on u.DepartmentId equals d.Id
                        join r in _context.Roles on u.RoleId equals r.Id
                        where u.Status == UserStatus.Working
                        select new GetUserVm
                        {
                            Id = u.Id,
                            FullName = u.FullName,
                            Username = u.Username,
                            RoleName = r.Name,
                            DepartmentName = d.Name,
                            RoleId = r.Id,
                            DepartmentId = d.Id
                        };
            var data = await query.OrderBy(c => c.Id).ToListAsync();
            return Ok(data);
        }

        [HttpPost("RegisterUserVm")]
        public async Task<IActionResult> RegisterUserVm(CreateUserVm userVm)
        {
            User user = new User();
            user.FullName = userVm.FullName;
            user.Username = userVm.Username;
            user.DepartmentId = userVm.DepartmentId;
            user.RoleId = userVm.RoleId;
            CreatePasswordHash(userVm.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Username = userVm.Username;
            UserValidator validator = new UserValidator();
            var result = validator.Validate(user);
            if (result.IsValid)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                List<RolePermission> rolePermissions = await _context.RolePermissions.Where(c => c.RoleId == user.RoleId).ToListAsync();
                foreach (RolePermission rolePermission in rolePermissions)
                {
                    UserPermission userPermission = new UserPermission();
                    userPermission.FunctionId = rolePermission.FunctionId;
                    userPermission.UserId = user.Id;
                    userPermission.Command = rolePermission.Command;
                    userPermission.Status = true;
                    _context.UserPermissions.Add(userPermission);
                }
                await _context.SaveChangesAsync();
            }
            else return BadRequest($"Property '{result.Errors[0].PropertyName}': {result.Errors[0].ErrorMessage}");
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            User res = await _context.Users.FindAsync(id);
            if (res != null)
            {
                res.Status = UserStatus.Delete;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else return NotFound();
        }

        #endregion Public Methods
    }
}