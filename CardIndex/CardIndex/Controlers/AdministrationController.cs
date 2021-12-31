using Administration.Exceptions;
using Administration.HelperModels;
using Administration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CardIndex.Controlers
{
    /// <summary>
    ///The controller is 
    ///designed to implement REST functions 
    ///over the entities UserIdentity and RoleIdentity
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AdministrationController> _logger;

        public AdministrationController(IUserService userService, ILogger<AdministrationController> logger)
        {
            _userService = userService;
            _logger = logger;
            _logger.LogInformation("Administration controller was created");
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            _logger.LogInformation("Was called RegisterAsync method from Administration Controller");
            try
            {
                await _userService.RegisterAsync(model);
                _logger.LogInformation("Method RegisterAsync from Administration Controller was SUCCESSFULL finished");
                return Created(string.Empty, string.Empty);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method RegisterAsync from Administration Controller was FAILED: " +
                    " Entered user data is invalid");
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("logon")]
        public async Task<IActionResult> LogonAsync(LogonModel model)
        {
            _logger.LogInformation("Was called LogonAsync method from Administration Controller");
            try
            {
                var JwtToken = await _userService.LogonAsync(model);
                _logger.LogInformation("Method LogonAsync from Administration Controller was SUCCESSFULL finished");
                return Ok(JwtToken);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method LogonAsync from Administration Controller was FAILED: " +
                 " There is no user in database with entered data");
                return NotFound(ex.Message);
            }

        }

        [HttpPost("createRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleModel model)
        {
            _logger.LogInformation("Was called CreateRoleAsync method from Administration Controller");
            try
            {
                await _userService.CreateRoleAsync(model.RoleName);
                _logger.LogInformation("Method CreateRoleAsync from Administration Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method CreateRoleAsync from Administration Controller was FAILED: " +
                 " Entered role data is invalid");
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getRoles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRolesAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetRolesAsync method from Administration Controller");
            return Ok(await _userService.GetRolesAsync());
        }

        /// <summary>
        /// The method is designed to give the user different access rights
        /// </summary>
        /// <param name="model">Model that includes the user's email and specified access rights</param>
        /// <returns></returns>
        [HttpPost("assignUserToRoles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignUserToRoleAsync(AssignUserToRolesModel model)
        {
            _logger.LogInformation("Was called AssignUserToRoleAsync method from Administration Controller");
            try
            {
                await _userService.AssignUserToRolesAsync(model);
                _logger.LogInformation("Method AssignUserToRoleAsync from Administration Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method AssignUserToRoleAsync from Administration Controller was FAILED: " +
                " Entered data about user or role is invalid");
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteUser/{email}/{password}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserAsync(string email, string password)
        {
            _logger.LogInformation("Was called DeleteUserAsync method from Administration Controller");
            try
            {
                var res = await _userService.DeleteUserByEmailAndPasswordAsync(email, password);
                _logger.LogInformation("Method DeleteUserAsync from Administration Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteUserAsync from Administration Controller was FAILED: " +
                 " There is no user in database with entered data");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser/{email}/{password}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserAsync(string email, string password)
        {
            _logger.LogInformation("Was called GetUserAsync method from Administration Controller");
            try
            {
                var res = await _userService.GetUserByEmailAndPasswordAsync(email, password);
                _logger.LogInformation("Method GetUserAsync from Administration Controller was SUCCESSFULL finished");
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method GetUserAsync from Administration Controller was FAILED: " +
                " There is no user in database with entered data");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllUsersAsync method from Administration Controller");
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpDelete("DeleteRole/{roleName}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRoleAsync(string roleName)
        {
            _logger.LogInformation("Was called DeleteRoleAsync method from Administration Controller");
            try
            {
                var res = await _userService.DeleteRoleAsync(roleName);
                _logger.LogInformation("Method DeleteRoleAsync from Administration Controller was SUCCESSFULL finished");
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteRoleAsync from Administration Controller was FAILED: " +
                " There is no role in database with entered data");
                return NotFound(ex.Message);
            }
        }
    }
}
