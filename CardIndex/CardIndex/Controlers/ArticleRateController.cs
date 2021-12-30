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
    public class ArticleRateController : ControllerBase
    {
        private readonly IBaseService<ArticleRateAddModel, ArticleRateVievModel> _articleRateService;

        public ArticleRateController(IBaseService<ArticleRateAddModel, ArticleRateVievModel> articleRateService)
        {
            _articleRateService = articleRateService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleRateVievModel>>> GetAll()
        {
            return Ok(await _articleRateService.GetAllWithDetails());
        }


        [HttpPost]
        public async Task<ActionResult<ArticleRateVievModel>> Add([FromBody] ArticleRateAddModel articleModel)
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
