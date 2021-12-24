using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;
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
        private readonly IService<ThemeModel> _themeService;

        public ThemeController(IService<ThemeModel> themeService)
        {
            _themeService = themeService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ThemeModel>> GetAll()
        {
            return Ok(_themeService.GetAllWithDetails());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ThemeModel>> GetById(int id)
        {
            try
            {
                var articleRateModel = await _themeService.GetByIdAsync(id);
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
        public async Task<IActionResult> Add([FromBody] ThemeModel articleModel)
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

        // PUT api/<ArticleController>/5
        [HttpPut]
        public ActionResult<ThemeModel> Update([FromBody] ThemeModel value)
        {
            try
            {
                var result = _themeService.Update(value);
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
                _themeService.Delete(id);
                return Ok();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
