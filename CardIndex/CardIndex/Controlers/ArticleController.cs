using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using BLL.Exceptions;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace CardIndex.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_articleService.GetAllWithDetails());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var articleModel = await _articleService.GetByIdAsync(id);
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{theme}")]
        public ActionResult<ArticleModel> GetByTheme(string theme)
        {
            try
            {
                var articleModel = _articleService.GetByTheme(theme);
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{name}")]
        public ActionResult<ArticleModel> GetByName(string name)
        {
            try
            {
                var articleModel = _articleService.GetByName(name);
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("avgRate/{name}")]
        public ActionResult<double> GetAvgRateByName(string name)
        {
            try
            {
                var Rate = _articleService.GetAvgRate(name);
                return Ok(Rate);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<ArticleController>
        ///
        [HttpPost]
        public async Task<ActionResult<ArticleModel>> Add([FromBody] ArticleModel articleModel)
        {
            try
            {
                var result = await _articleService.AddAsync(articleModel);
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<ArticleController>/5
        [HttpPut]
        public ActionResult<ArticleModel> Update([FromBody] ArticleModel value)
        {
            try
            {
                var result = _articleService.Update(value);
                return Ok(result);
            }
            catch (InvalidArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            try
            {
                _articleService.Delete(id);
                return Ok();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
