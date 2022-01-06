using Administration.Exceptions;
using BLL.AddModels;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<AdministrationController> _logger;

        public ArticleController(IArticleService articleService, ILogger<AdministrationController> logger)
        {
            _articleService = articleService;
            _logger = logger;
            _logger.LogInformation("Aricle controller was created");
        }


        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Was SUCCESSFULL called GetAllAsync method from Aricle Controller");
            return Ok(await _articleService.GetAllWithDetailsAsync());
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "admin,")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Was called GetByIdAsync method from Aricle Controller");
            try
            {
                var articleModel = await _articleService.GetByIdWithDetailsAsync(id);
                _logger.LogInformation("Method GetByIdAsync from Aricle Controller was SUCCESSFULL finished");
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method GetByIdAsync from Aricle Controller was FAILED: " +
                " There is no article in database with entered id");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getByTheme{theme}")]
        //[Authorize]
        public async Task<IActionResult> GetByThemeAsync(string theme)
        {
            _logger.LogInformation("Was called GetByThemeAsync method from Aricle Controller");
            try
            {
                var articleModel = await _articleService.GetByThemeAsync(theme);
                _logger.LogInformation("Method GetByThemeAsync from Aricle Controller was SUCCESSFULL finished");
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method GetByThemeAsync from Aricle Controller was FAILED: " +
                " There is no articles in database with entered theme");
                return NotFound(ex.Message);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method GetByThemeAsync from Aricle Controller was FAILED: " +
                " Entered theme doesn't exist");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getByName/{name}")]
        //[Authorize]
        public async Task<IActionResult> GetByTitleAsync(string name)
        {
            _logger.LogInformation("Was called GetByNameAsync method from Aricle Controller");
            try
            {
                var articleModel = await _articleService.GetByNameAsync(name);
                _logger.LogInformation("Method GetByNameAsync from Aricle Controller was SUCCESSFULL finished");
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method GetByNameAsync from Aricle Controller was FAILED: " +
                " There is no article in database with entered name");
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// The method is designed to 
        /// obtain all articles with a rating 
        /// ranging from 50 to the specified 
        /// number of characters
        /// </summary>
        /// <param name="length">limit lenght of article</param>
        /// <returns></returns>
        [HttpGet("getByLength/{length}")]
        //[Authorize]
        public async Task<IActionResult> GetByLenghtAsync(int length)
        {
            var articleModel = await _articleService.GetByLengthAsync(length);
            _logger.LogInformation("Was SUCCESSFULL called GetByLenghtAsync method from Aricle Controller");
            return Ok(articleModel);
        }

        /// <summary>
        /// The method is designed 
        /// to obtain all articles whose rating 
        /// falls within the specified period
        /// </summary>
        /// <param name="max">max value of rating</param>
        /// <param name="min">min value of rating</param>
        /// <returns></returns>
        [HttpGet("getByRangeOfRate/{max}/{min}")]
        //[Authorize]
        public async Task<IActionResult> GetByRangeOfRateAsync(double max, double min)
        {
            var articleModel = await _articleService.GetByRangeOfRateAsync(max, min);
            _logger.LogInformation("Was SUCCESSFULL called GetByRangeOfRateAsync method from Aricle Controller");
            return Ok(articleModel);
        }

        [HttpPost("AddAricle")]
        //[Authorize(Roles = "author")]
        public async Task<IActionResult> AddAsync([FromBody] ArticleAddmodel articleModel)
        {
            _logger.LogInformation("Was called AddAsync method from Aricle Controller");
            try
            {
                var result = await _articleService.AddAsync(articleModel);
                _logger.LogInformation("Method AddAsync from Aricle Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method AddAsync from Aricle Controller was FAILED: " +
                " TEntered article data is invalid");
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                _logger.LogWarning("Method AddAsync from Aricle Controller was FAILED: " +
                " Entered article already exist in database");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateArticle")]
        //[Authorize(Roles = "author,admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] ArticleAddmodel value)
        {
            _logger.LogInformation("Was called UpdateAsync method from Aricle Controller");
            try
            {
                var result = await _articleService.UpdateAsync(value);
                _logger.LogInformation("Method UpdateAsync from Aricle Controller was SUCCESSFULL finished");
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogWarning("Method UpdateAsync from Aricle Controller was FAILED: " +
                " Entered article data is invalid");
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method UpdateAsync from Aricle Controller was FAILED: " +
                " There is no article to update in database with entered data");
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "author,admin")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Was called DeleteByIdAsync method from Aricle Controller");
            try
            {
                await _articleService.DeleteAsync(id);
                _logger.LogInformation("Method DeleteByIdAsync from Aricle Controller was SUCCESSFULL finished");
                return Ok();

            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Method DeleteByIdAsync from Aricle Controller was FAILED: " +
                " There is no article to delete in database with entered id");
                return BadRequest(ex.Message);
            }
        }
    }
}
