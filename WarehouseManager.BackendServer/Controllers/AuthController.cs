using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarehouseManager.BackendServer.Data;
using WarehouseManager.BackendServer.Data.Entities;
using WarehouseManager.ViewModels;
using WarehouseManager.ViewModels.Admin.User;
using WarehouseManager.ViewModels.Constants;

namespace WarehouseManager.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Init

        private readonly WhContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(WhContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #endregion Init

        #region Private

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var query = from up in _context.UserPermissions
                        where up.UserId == user.Id && up.Status == true
                        select up.FunctionId + "_" + up.Command;
            var permissions = await query.Distinct().ToListAsync();
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(SystemConstants.Permissions, JsonConvert.SerializeObject(permissions))
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetSection("AppSettings:Issuer").Value,
                Audience = _configuration.GetSection("AppSettings:Audience").Value,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        #endregion Private

        #region Public

        [Authorize("Bearer")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserLoginVm userVm)
        {
            User user = new User();
            CreatePasswordHash(userVm.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Username = userVm.Username;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [Authorize("Bearer")]
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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginVm userVm)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(userVm.Username.ToLower()));
            if (user == null)
                return BadRequest($"User {userVm.Username} không tồn tại");
            else if (!VerifyPasswordHash(userVm.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Sai mật khẩu!");
            else
            {
                string token = await CreateTokenAsync(user);
                return Ok(token);
            }
        }

        [Authorize("Bearer")]
        [HttpGet("Logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        #endregion Public
    }
}