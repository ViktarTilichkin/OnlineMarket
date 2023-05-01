using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models.ApiRequest;
using OnlineMarket.Models.Options;
using OnlineMarket.Services;

namespace OnlineMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService m_AccountService;
        public AccountController(AccountService service)
        {
            m_AccountService = service;
        }
        [HttpPost("[action]")]
        public async Task<TokenData> Login(Login user)
        {
            return await m_AccountService.Token(user.Email, user.Pwd);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
