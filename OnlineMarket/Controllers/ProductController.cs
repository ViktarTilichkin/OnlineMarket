using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models.Repository;
using OnlineMarket.Services;

namespace OnlineMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService m_ProductService;
        public ProductController(ProductService productService)
        {
            m_ProductService = productService;
        }
        [ResponseCache(Duration = 20)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await m_ProductService.GetAll());
            }
            catch (Exception ex)  
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            //try
            //{
            //    return Ok(await m_ProductService.GetById(id));
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            return Ok(await m_ProductService.GetById(id));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser()
        {
            try
            {
                return Ok(await m_ProductService.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser()
        {
            try
            {
                return Ok(await m_ProductService.Update());
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
                return Ok(await m_ProductService.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
