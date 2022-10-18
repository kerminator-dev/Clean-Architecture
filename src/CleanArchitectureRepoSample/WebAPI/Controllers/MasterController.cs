using Application.Common.Interface;
using Application.Master;
using Domain.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appSettings = await _masterService.GetAppSettingsAsync();

            return Ok(appSettings);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AppSetting appSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _masterService.UpsertAsync(appSetting);

            return Ok(appSetting);
        }
    }
}
