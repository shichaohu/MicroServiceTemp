using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        /// <summary>
        /// 获取资源使用率
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ResourcesRate")]
        public string GetResourcesRate()
        {
            string cpuRate;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var output = ShellUtil.Bash("top -b -n1 | grep \"Cpu(s)\" | awk '{print $2 + $4}'");
                cpuRate = output.Trim();
            }
            else
            {
                var output = ShellUtil.Cmd("wmic", "cpu get LoadPercentage");
                cpuRate = output.Replace("LoadPercentage", string.Empty).Trim();
            }
            return cpuRate;
        }

        /// <summary>
        /// 生成dump文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("createDumpFile")]
        public bool CreateDumpFile()
        {
            throw new System.Exception("to create dump file!");
            return true;
        }


    }
}
