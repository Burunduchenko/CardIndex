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
    public class ArticleRateController : ControllerBase
    {
        private readonly IService<ArticleRateModel> _articleRateService;

        public ArticleRateController(IService<ArticleRateModel> articleRateService)
        {
            _articleRateService = articleRateService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ArticleRateModel>> GetAll()
        {
            return Ok(_articleRateService.GetAllWithDetails());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleRateModel>> GetById(int id)
        {
            try
            {
                var articleRateModel = await _articleRateService.GetByIdAsync(id);
                return Ok(articleRateModel);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // POST api/<ArticleController>
        ///
        [HttpPost]
        public async Task<ActionResult<ArticleRateModel>> Add([FromBody] ArticleRateModel articleModel)
        {
            try
            {
                var result = await _articleRateService.AddAsync(articleModel);
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
        public ActionResult<ArticleRateModel> Update([FromBody] ArticleRateModel value)
        {
            try
            {
                var result = _articleRateService.Update(value);
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
                _articleRateService.Delete(id);
                return Ok();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
