using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Services;
using BLL.Exceptions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using BLL.VievModels;
using BLL.AddModels;

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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _articleService.GetAllWithDetails());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var articleModel = await _articleService.GetByIdWithDetailsAsync(id);
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getByTheme{theme}")]
        public async Task<ActionResult<IEnumerable<ArticelVievModel>>> GetByTheme(string theme)
        {
            try
            {
                var articleModel = await _articleService.GetByTheme(theme);
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

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<ArticelVievModel>> GetByName(string name)
        {
            try
            {
                var articleModel = await _articleService.GetByName(name);
                return Ok(articleModel);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getByLength/{length}")]
        public async Task<ActionResult<IEnumerable<ArticelVievModel>>> GetByLenght(int length)
        {
            var articleModel = await _articleService.GetByLength(length);
            return Ok(articleModel);
        }


        [HttpGet("getByRangeOfRate/{max}/{min}")]
        public async Task<ActionResult<IEnumerable<ArticelVievModel>>> GetByRangeOfRate(double max, double min)
        {
            var articleModel = await _articleService.GetByRangeOfRate(max, min);
            return Ok(articleModel);
        }


        // POST api/<ArticleController>
        
        [HttpPost]
        public async Task<ActionResult<ArticelVievModel>> Add([FromBody] ArticleAddmodel articleModel)
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
        public ActionResult<ArticelVievModel> Update([FromBody] ArticleAddmodel value)
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
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                await _articleService.Delete(id);
                return Ok();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
