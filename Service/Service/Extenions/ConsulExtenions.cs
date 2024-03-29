﻿using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServiceBase.Extenions
{
    public static class ConsulExtenions
    {
        public static void ConsulRegist(this IConfiguration configuration)
        {
            try
            {
                string ip = configuration["ip"];
                string port = configuration["port"];
                string weight = configuration["weight"];
                string consulAddress = configuration["ConsulAddress"];
                string consulCenter = configuration["ConsulCenter"];

                ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);
                    c.Datacenter = consulCenter;
                });

                var result = client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = $"MicroService {ip}:{port}",//--唯一的
                    Name = $"MicroService",//分组---根据Service
                    Address = ip,
                    Port = int.Parse(port),
                    Tags = new string[] { weight.ToString() },//额外标签信息
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(12),
                        HTTP = $"http://{ip}:{port}/HealthCheck", // 给到200
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                    }//配置心跳
                });
                result.Wait();
                Console.WriteLine(result.Result.ToString());
                Console.WriteLine($"{ip}:{port}--weight:{weight}"); //命令行参数获取
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Consul注册：{ex.Message}");
            }
        }
    }
}
