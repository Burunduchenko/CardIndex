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
    public class UserController : ControllerBase
    {
        private readonly IService<UserModel> _userService;


        public UserController(IService<UserModel> userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAll()
        {
            return Ok(_userService.GetAllWithDetails());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            try
            {
                var articleRateModel = await _userService.GetByIdAsync(id);
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
        public async Task<ActionResult<UserModel>> Add([FromBody] UserModel articleModel)
        {
            try
            {
                var result = await _userService.AddAsync(articleModel);
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
        public ActionResult<UserModel> Update([FromBody] UserModel value)
        {
            try
            {
                var result = _userService.Update(value);
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
                _userService.Delete(id);
                return Ok();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
