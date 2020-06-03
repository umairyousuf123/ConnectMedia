using ConnectMedia.Common.DTO;
using ConnectMedia.Common.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AndroidController : ControllerBase
    {
        IHttpContextAccessor _httpContextAccessor;
        IAndroidService _androidService;
        readonly ILogger<AndroidController> _logger;

        public AndroidController(ILogger<AndroidController> logger, IAndroidService androidService)
        {
            _logger = logger;
            this._androidService = androidService;
        }
        [HttpPost("Login")]
        public async Task<DataTransferObject<string>> Login(LoginRequestDTO data)
        {
            return await Task.Run(() =>
            {
                return _androidService.UserLogin(new LoginRequestDTO());
            });

        }
        [HttpGet("TestApi")]
        public string TestApi()
        {
            return "Yes";
        }
        [Authorize]
        [HttpGet("LogoutUserFromSystem")]
        public void LogoutUserFromSystem()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }
        [AllowAnonymous]
        [HttpGet("GetBuildingPlaylist")]
        public IActionResult GetBuildingPlaylist(string key)
        {
            List<NoticeDTO> notices = _androidService.GetPlatlistFromBuilding(key);
            if (notices == null)
            {
                return NotFound();
            }
            return Ok(notices);
           
        }
    }
}