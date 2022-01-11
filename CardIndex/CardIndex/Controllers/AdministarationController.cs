using Administration;
using Administration.Exceptions;
using Administration.HelperModels;
using Administration.Interfaces;
using Administration.VievModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
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
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterModel model)
        {
            _logger.LogInformation("Was called RegisterAsync method from Administration Controller");
            try
            {
                await _userService.RegisterAsync(model);
                _logger.LogInformation("Method RegisterAsync from Administration Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method RegisterAsync from Administration Controller was FAILED: " +
                    " Entered user data is invalid");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from RegisterAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpPost("logon")]
        public async Task<IActionResult> LogonAsync([FromBody]LogonModel model)
        {
            _logger.LogInformation("Was called LogonAsync method from Administration Controller");
            try
            {
                var JwtToken = await _userService.LogonAsync(model);
                var result = JsonSerializer.Serialize(JwtToken);
                _logger.LogInformation("Method LogonAsync from Administration Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method LogonAsync from Administration Controller was FAILED: " +
                 " There is no user in database with entered data");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from LogonAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpPost("createRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleViev model)
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
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from CreateRoleAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
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
        /// The method is designed to GIVE the user different access rights
        /// </summary>
        /// <param name="model">Model that includes the user's email and specified access rights</param>
        /// <returns></returns>
        [HttpPost("provideUserToRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ProvideUserToRoleAsync([FromBody] ManipWithUserRole model)
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
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from ProvideUserToRoleAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }

        }


        /// <summary>
        /// The method is designed to TAKE AWAY the user different access rights
        /// </summary>
        /// <param name="model">Model that includes the user's email and specified access rights</param>
        /// <returns></returns>
        [HttpPost("takeUserFromRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> TakeUserFromRoleAsync([FromBody] ManipWithUserRole model)
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
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from TakeUserFromRoleAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUserAsync([FromQuery]string id)
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
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from DeleteUserAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("GetUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserAsync([FromQuery]string email, [FromQuery]string password)
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
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from GetUserAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllUsersAsync method from Administration Controller");
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpDelete("DeleteRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRoleAsync([FromQuery]string id)
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
            catch(Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from DeleteRoleAsync method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user)
        {
            _logger.LogInformation("Was called UpdateUser method from Administration Controller");
            try
            {
                var result = await _userService.UpdateUserAsync(user);
                _logger.LogInformation("Method UpdateUser from Administration Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method UpdateUser from Administration Controller was FAILED: " +
                " There is no role in database with entered data");
                return BadRequest(ex.Massege);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from UpdateUser method, Administration Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
