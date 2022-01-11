using Administration.Exceptions;
using BLL.AddModels;
using BLL.Interfaces;
using BLL.VievModels;
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
    ///over the entity ArticleRate
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleRateController : ControllerBase
    {
        private readonly IBaseService<ArticleRateAddModel, ArticleRateVievModel> _articleRateService;
        private readonly ILogger<AdministrationController> _logger;

        public ArticleRateController(IBaseService<ArticleRateAddModel, ArticleRateVievModel> articleRateService,
            ILogger<AdministrationController> logger)
        {
            _articleRateService = articleRateService;
            _logger = logger;
            _logger.LogInformation("Article Rate controller was created");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllAsync method from Article Rate Controller");
            return Ok(await _articleRateService.GetAllWithDetailsAsync());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync([FromBody] ArticleRateAddModel articleModel)
        {
            _logger.LogInformation("Was called AddAsync method from Aricle Rate Controller");
            try
            {
                var result = await _articleRateService.AddAsync(articleModel);
                _logger.LogInformation("Method AddAsync from Aricle Rate Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method AddAsync from Aricle Rate Controller was FAILED: " +
                " Entered article rate data is invalid");
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                _logger.LogWarning("Method AddAsync from Aricle Rate Controller was FAILED: " +
                " article rate already exist in database");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from AddAsync method, Article Rate Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteByIdAsync([FromQuery] int id)
        {
            _logger.LogInformation("Was called DeleteByIdAsync method from Aricle Rate Controller");
            try
            {
                await _articleRateService.DeleteAsync(id);
                _logger.LogInformation("Method DeleteByIdAsync from Aricle Rate Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteByIdAsync from Aricle Rate Controller was FAILED: " +
                " There is no article rate to delete in database with entered id");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from DeleteByIdAsync method, Article Rate Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }
    }
}

