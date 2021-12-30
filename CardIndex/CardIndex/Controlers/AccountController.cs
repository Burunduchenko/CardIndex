using Administration.Account;
using CardIndex.AccountModels;
using CardIndex.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CardIndex.Controlers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;

        public AccountController(
            IUserService userService,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            await _userService.Register(new Register
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Login = model.Login,
                PhoneNumber = model.PhoneNumber,
            });

            return Created(string.Empty, string.Empty);
        }

        [HttpPost("logon")]
        public async Task<IActionResult> Logon(LogonModel model)
        {
            var user = await _userService.Logon(new Logon
            {
                Email = model.Email,
                Password = model.Password
            });

            if (user is null) return BadRequest();

            var roles = await _userService.GetRoles(user);

            return Ok(JwtHelper.GenerateJwt(user, roles, _jwtSettings));
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            await _userService.CreateRole(model.RoleName);
            return Ok();
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _userService.GetRoles());
        }

        [HttpPost("assignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRoleModel model)
        {
            await _userService.AssignUserToRoles(new AssignUserToRoles
            {
                Email = model.Email,
                Roles = model.Roles
            });

            return Ok();
        }

        [HttpDelete("DeleteUser/{email}/{password}")]
        public async Task<IActionResult> DeleteUser(string email, string password)
        {
            try
            {
                var res = await _userService.DeleteUserByEmailAndPassword(email, password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser/{email}/{password}")]
        public async Task<IActionResult> GetUser( string email,  string password)
        {
            try
            {
                var res = await _userService.GetUserByEmailAndPassword(email, password);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpDelete("DeleteRole/{roleName}")]
        public async Task<IActionResult> DeleteRole( string roleName)
        {
            try
            {
                var res = await _userService.DeleteRole(roleName);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
