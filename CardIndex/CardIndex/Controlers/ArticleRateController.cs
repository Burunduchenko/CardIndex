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
        private readonly IBaseService<ArticleRateModel> _articleRateService;

        public ArticleRateController(IBaseService<ArticleRateModel> articleRateService)
        {
            _articleRateService = articleRateService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ArticleRateModel>> GetAll()
        {
            return Ok(_articleRateService.GetAllWithDetails());
        }


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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                await _articleRateService.Delete(id);
                return Ok();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
