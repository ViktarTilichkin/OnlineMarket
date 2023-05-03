using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models.Repository;
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
        [HttpGet("{i}")]
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
        [HttpPost("split/{split}")]
        public IActionResult SplitArray(int split, [FromBody] int[] array)
        {
            try
            {
                //List<List<int>> result = new List<List<int>>();
                //for (int i = 0; i < array.Length; i += split)
                //{
                //    List<int> list = new List<int>();
                //    for (int j = 1; j <= split; j++)
                //    {
                //        if (i + j - 1 < array.Length) list.Add(array[i + j - 1]);
                //    }
                //    result.Add(list);
                //}


                //List<List<int>> result = new List<List<int>>() { new List<int>() };
                //for (int i = 0; i < array.Length; i++)
                //{
                //    var itemresult = result.Last();
                //    itemresult.Add(array[i]);
                //    if (itemresult.Count == split && i != array.Length - 1)
                //    {
                //        result.Add(itemresult);
                //    }
                //}

                //var result = array.Select((value, index) => new { Index = index, Value = value }).GroupBy(x => x.Index/split).Select(o => o.Select(y => y.Value));

                //var result = array.Select((value, index) => new { index, value }).GroupBy(x => x.index / split).Select(o => o.Select(y => y.value));

                var result = array.Chunk(split);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
