using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarket.Services;

namespace OnlineMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService m_Service;

        public UserController(UserService rep)
        {
            m_Service = rep;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await m_Service.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                return Ok(await m_Service.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                return Ok(await m_Service.Create(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                return Ok(await m_Service.Update(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return Ok(await m_Service.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
