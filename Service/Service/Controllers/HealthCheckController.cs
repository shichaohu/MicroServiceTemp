using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// 健康检查地址
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
