using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPI.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUser _user;
        /// <summary>
        ///  Get Users
        /// </summary>
        /// <param name="user"></param>
        public UserController(IUser user)
        {
            _user = user;
        }
        // GET: api/<User>
        [HttpGet("List")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _user.GetAll());
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Save User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] Model.User user)
        {
            if (user == null)
                return BadRequest(ModelState);

            if(!await this._user.Create(user))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<User>/5
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
           if( await _user.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
