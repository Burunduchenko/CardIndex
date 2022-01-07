using Administration;
using Administration.Exceptions;
using Administration.HelperModels;
using Administration.Interfaces;
using Administration.VievModels;
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
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRoleAsync(RoleViev model)
        {
            _logger.LogInformation("Was called CreateRoleAsync method from Administration Controller");
            try
            {
                await _userService.CreateRoleAsync(model.Name);
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
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRolesAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetRolesAsync method from Administration Controller");
            return Ok(await _userService.GetRolesAsync());
        }

        /// <summary>
        /// The method is designed to GIVE the user different access rights
        /// </summary>
        /// <param name="model">Model that includes the user's email and specified access rights</param>
        /// <returns></returns>
        [HttpPost("provideUserToRole")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> ProvideUserToRoleAsync(ManipWithUserRole model)
        {
            _logger.LogInformation("Was called ProvideUserToRoleAsync method from Administration Controller");
            try
            {
                await _userService.ProvideUserToRoleAsync(model);
                _logger.LogInformation("Method ProvideUserToRoleAsync from Administration Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method ProvideUserToRoleAsync from Administration Controller was FAILED: " +
                " Entered data about user or role is invalid");
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// The method is designed to TAKE AWAY the user different access rights
        /// </summary>
        /// <param name="model">Model that includes the user's email and specified access rights</param>
        /// <returns></returns>
        [HttpPost("takeUserFromRole")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> TakeUserFromRoleAsync(ManipWithUserRole model)
        {
            _logger.LogInformation("Was called AssignUserToRoleAsync method from Administration Controller");
            try
            {
                await _userService.TakeUserFromRoleAsync(model);
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

        [HttpDelete("DeleteUser/{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            _logger.LogInformation("Was called DeleteUserAsync method from Administration Controller");
            try
            {
                var res = await _userService.DeleteUserByIdAsync(id);
                _logger.LogInformation("Method DeleteUserAsync from Administration Controller was SUCCESSFULL finished");
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteUserAsync from Administration Controller was FAILED: " +
                 " There is no user in database with entered data");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser/{email}/{password}")]
        //[Authorize(Roles = "admin")]
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
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllUsersAsync method from Administration Controller");
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpDelete("DeleteRole/{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            _logger.LogInformation("Was called DeleteRoleAsync method from Administration Controller");
            try
            {
                var res = await _userService.DeleteRoleAsync(id);
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

        [HttpPut("Update")]
        //[Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(user);
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                return BadRequest(ex.Massege);
            }
        }
    }
}
