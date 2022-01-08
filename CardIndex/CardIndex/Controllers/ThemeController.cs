using Administration.Exceptions;
using BLL.AddModels;
using BLL.Interfaces;
using BLL.VievModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CardIndex.Controlers
{
    /// <summary> 
    ///The controller is 
    ///designed to implement REST functions 
    ///over the entity Theme
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IBaseService<ThemeAddModel, ThemeVievModel> _themeService;
        private readonly ILogger<AdministrationController> _logger;

        public ThemeController(IBaseService<ThemeAddModel, ThemeVievModel> themeService, ILogger<AdministrationController> logger)
        {
            _themeService = themeService;
            _logger = logger;
            _logger.LogInformation("Theme controller was created");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllAsync method from Theme Controller");
            return Ok(await _themeService.GetAllWithDetailsAsync());
        }

        [HttpPost]
        [Authorize(Roles = "admin,author")]
        public async Task<IActionResult> AddAsync([FromBody] ThemeAddModel articleModel)
        {
            _logger.LogInformation("Was called AddAsync method from Theme Controller");
            try
            {
                var result = await _themeService.AddAsync(articleModel);
                _logger.LogInformation("Method AddAsync from Theme Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method AddAsync from Theme Controller was FAILED: " +
                " Entered theme data is invalid");
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                _logger.LogWarning("Method AddAsync from Theme Controller was FAILED: " +
                " Entered theme already exist in database");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin,author")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Was called DeleteByIdAsync method from Theme Controller");
            try
            {
                await _themeService.DeleteAsync(id);
                _logger.LogInformation("Method DeleteByIdAsync from Theme Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteByIdAsync from Theme Controller was FAILED: " +
                " There is no theme to delete in database with entered id");
                return BadRequest(ex.Message);
            }
        }
    }
}
