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
    public class CardAssessmentController : ControllerBase
    {
        private readonly IBaseService<CardAssessmentAddModel, CardAssementVievModel> _cardAssessmentService;
        private readonly ILogger<AdministrationController> _logger;

        public CardAssessmentController(IBaseService<CardAssessmentAddModel, CardAssementVievModel> cardAssessmentService,
            ILogger<AdministrationController> logger)
        {
            _cardAssessmentService = cardAssessmentService;
            _logger = logger;
            _logger.LogInformation("Card Assessment controller was created");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllAsync method from Card Assessment Controller");
            return Ok(await _cardAssessmentService.GetAllWithDetailsAsync());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync([FromBody] CardAssessmentAddModel cardAssessmentModel)
        {
            _logger.LogInformation("Was called AddAsync method from Card Assessment Controller");
            try
            {
                var result = await _cardAssessmentService.AddAsync(cardAssessmentModel);
                _logger.LogInformation("Method AddAsync from Card Assessment Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method AddAsync from Card Assessment Controller was FAILED: " +
                " Entered article rate data is invalid");
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                _logger.LogWarning("Method AddAsync from Card AssessmentController was FAILED: " +
                " article rate already exist in database");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from AddAsync method, Card Assessment Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpDelete("delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteByIdAsync([FromQuery] int id)
        {
            _logger.LogInformation("Was called DeleteByIdAsync method from Card Assessment Controller");
            try
            {
                await _cardAssessmentService.DeleteAsync(id);
                _logger.LogInformation("Method DeleteByIdAsync from Card Assessment Controller was SUCCESSFULL finished");
                return Ok();
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteByIdAsync from Card Assessment Controller was FAILED: " +
                " There is no article rate to delete in database with entered id");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from DeleteByIdAsync method, Card Assessment Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }
    }
}

