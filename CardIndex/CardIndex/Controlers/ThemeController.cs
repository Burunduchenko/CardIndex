using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.VievModels;
using BLL.AddModels;
using BLL.Services;
using BLL.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardIndex.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IBaseService<ThemeAddModel, ThemeVievModel> _themeService;

        public ThemeController(IBaseService<ThemeAddModel, ThemeVievModel> themeService)
        {
            _themeService = themeService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeVievModel>>> GetAll()
        {
            return Ok(await _themeService.GetAllWithDetails());
        }



        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ThemeAddModel articleModel)
        {
            try
            {
                var result = await _themeService.AddAsync(articleModel);
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
                await _themeService.Delete(id);
                return Ok();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
