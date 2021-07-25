using ApiClient.Extenions;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string OnGet()
        {
            AgentService agentService = ConsulClientExtenions.ChooseService("MicroService");
            string url = $"http://{agentService.Address}:{agentService.Port}/api/Comm/GetProvince";
            return url;
        }
    }
}
