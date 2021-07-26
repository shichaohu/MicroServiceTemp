using Microsoft.AspNetCore.Mvc;

namespace ApiServiceBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// 健康检查地址
        /// </summary>
        /// <returns></returns>
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
