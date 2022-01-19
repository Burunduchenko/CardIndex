using Administration.Exceptions;
using BLL.AddModels;
using BLL.Interfaces;
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
    ///over the entity Article 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILogger<AdministrationController> _logger;

        public CardController(ICardService cardService, ILogger<AdministrationController> logger)
        {
            _cardService = cardService;
            _logger = logger;
            _logger.LogInformation("Card controller was created");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllAsync method from Card Controller");
            return Ok(await _cardService.GetAllWithDetailsAsync());
        }

        [HttpGet("getByTheme")]
        [Authorize]
        public async Task<IActionResult> GetByThemeAsync([FromQuery] string theme)
        {
            _logger.LogInformation("Was called GetByThemeAsync method from Card Controller");
            try
            {
                var articleModel = await _cardService.GetByThemeAsync(theme);
                _logger.LogInformation("Method GetByThemeAsync from Card Controller was SUCCESSFULL finished");
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method GetByThemeAsync from Card Controller was FAILED: " +
                " There is no articles in database with entered theme");
                return NotFound(ex.Message);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method GetByThemeAsync from Card Controller was FAILED: " +
                " Entered theme doesn't exist");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from GetByThemeAsync method, Card Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("getByTitle")]
        [Authorize]
        public async Task<IActionResult> GetByTitleAsync([FromQuery] string title)
        {
            _logger.LogInformation("Was called GetByNameAsync method from Card Controller");
            try
            {
                var articleModel = await _cardService.GetByNameAsync(title);
                _logger.LogInformation("Method GetByNameAsync from Card Controller was SUCCESSFULL finished");
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method GetByNameAsync from Card Controller was FAILED: " +
                " There is no article in database with entered name");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from GetByTitleAsync method, Card Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// The method is designed to 
        /// obtain all cards with a rating 
        /// ranging from 50 to the specified 
        /// number of characters
        /// </summary>
        /// <param name="length">limit lenght of article</param>
        /// <returns></returns>
        [HttpGet("getByLength")]
        [Authorize]
        public async Task<IActionResult> GetByLenghtAsync([FromQuery] int length)
        {
            var articleModel = await _cardService.GetByLengthAsync(length);
            _logger.LogInformation("Was SUCCESSFULL called GetByLenghtAsync method from Card Controller");
            return Ok(articleModel);
        }

        /// <summary>
        /// The method is designed 
        /// to obtain all cards whose rating 
        /// falls within the specified period
        /// </summary>
        /// <param name="max">max value of rating</param>
        /// <param name="min">min value of rating</param>
        /// <returns></returns>
        [HttpGet("getByRangeOfRate")]
        [Authorize]
        public async Task<IActionResult> GetByRangeOfRateAsync([FromQuery]double max, [FromQuery] double min)
        {
            var articleModel = await _cardService.GetByRangeOfRateAsync(max, min);
            _logger.LogInformation("Was SUCCESSFULL called GetByRangeOfRateAsync method from Card Controller");
            return Ok(articleModel);
        }

        [HttpPost("AddCard")]
        [Authorize(Roles = "author")]
        public async Task<IActionResult> AddAsync([FromBody] CardAddmodel articleModel)
        {
            _logger.LogInformation("Was called AddAsync method from Card Controller");
            try
            {
                var result = await _cardService.AddAsync(articleModel);
                _logger.LogInformation("Method AddAsync from Card Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method AddAsync from Card Controller was FAILED: " +
                " TEntered article data is invalid");
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                _logger.LogWarning("Method AddAsync from Card Controller was FAILED: " +
                " Entered article already exist in database");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from AddAsync method, Card Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPut("UpdateCard")]
        [Authorize(Roles = "author,admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] CardAddmodel value)
        {
            _logger.LogInformation("Was called UpdateAsync method from Card Controller");
            try
            {
                var result = await _cardService.UpdateAsync(value);
                _logger.LogInformation("Method UpdateAsync from Card Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method UpdateAsync from Card Controller was FAILED: " +
                " Entered article data is invalid");
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method UpdateAsync from Card Controller was FAILED: " +
                " There is no article to update in database with entered data");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from UpdateAsync method, Card Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "author,admin")]
        public async Task<IActionResult> DeleteByIdAsync([FromQuery]int id)
        {
            _logger.LogInformation("Was called DeleteByIdAsync method from Card Controller");
            try
            {
                await _cardService.DeleteAsync(id);
                _logger.LogInformation("Method DeleteByIdAsync from Card Controller was SUCCESSFULL finished");
                return Ok();

            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteByIdAsync from Card Controller was FAILED: " +
                " There is no article to delete in database with entered id");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Was throwed unexpected Exception from DeleteByIdAsync method, Card Controller: " +
                    $"{ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
