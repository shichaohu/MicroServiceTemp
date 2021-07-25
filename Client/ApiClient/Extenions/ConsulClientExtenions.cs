using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient.Extenions
{
    public static class ConsulClientExtenions
    {
        public static AgentService ChooseService(string serviceName)
        {
            using (ConsulClient client = new ConsulClient(c => c.Address = new Uri("http://47.100.188.143:8500")))
            {
                var services = client.Agent.Services().Result.Response;
                // 找出目标服务
                var targetServices = services.Where(c => c.Value.Service.Equals(serviceName)).Select(s => s.Value);

                int index = 0;
                #region 负载均衡
                //随机
                index=new Random().Next(1, 1000) % targetServices.Count();
                //轮询
                //index=
                #endregion

                var targetService = targetServices.ElementAt(index);
                Console.WriteLine($"{DateTime.Now} 当前调用服务为：{targetService.Address}:{targetService.Port}");

                return targetService;
            }
        }
    }
}
