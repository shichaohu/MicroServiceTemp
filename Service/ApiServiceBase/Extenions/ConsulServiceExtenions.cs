using Consul;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServiceBase.Extenions
{
    public static class ConsulServiceExtenions
    {
        public static void RegisterServiceToConsul(this IConfiguration configuration)
        {
            try
            {
                string ip = configuration["Consul:ServiceIp"];
                string port = configuration["Consul:ServicePort"];
                string weight = configuration["Consul:ServiceWeight"];
                string consulName = configuration["Consul:ConsulName"];
                string consulAddress = configuration["Consul:ConsulAddress"];
                string consulCenter = configuration["Consul:ConsulCenter"];
                string healthCheckUrl = $"http://{ip}:{port}/HealthCheck";

                Console.WriteLine($"开始注册服务:{DateTime.Now}");
                Console.WriteLine($"serviceUrl:{ip}:{port},weight:{weight}");
                Console.WriteLine($"healthCheckUrl:{healthCheckUrl}");
                Console.WriteLine($"consulAddress:{consulAddress},consulCenter:{consulCenter},consulName:{consulName}");
                ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);
                    c.Datacenter = consulCenter;
                });

                var result = client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = $"{consulName} {ip}:{port}",//唯一的
                    Name = consulName,//分组名称
                    Address = ip,
                    Port = int.Parse(port),
                    Tags = new string[] { weight.ToString() },//额外标签信息，如权重
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(12),
                        HTTP = $"http://{ip}:{port}/HealthCheck",
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                    }//配置心跳
                });
                result.Wait();
                Console.WriteLine($"服务注册结果:{JsonConvert.SerializeObject(result.Result)}");
                Console.WriteLine($"服务注册完成:{DateTime.Now}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Consul注册({DateTime.Now})：{ex.Message}");
            }
        }
    }
}
