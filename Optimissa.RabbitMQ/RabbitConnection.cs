using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimissa.RabbitMQ
{
    public class RabbitConnection
    {
        private static readonly RabbitConnection _rabbitConnection = new RabbitConnection();
        IConfiguration _configuration;

        private string HostName;
        private string UserName;
        private string Password;

        public RabbitConnection(string sectionName = "RabbitCredentials")
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .Build();

            var rabbitCredentials = _configuration.GetSection(sectionName);

            HostName = rabbitCredentials["HostName"];
            UserName = rabbitCredentials["UserName"];
            Password = rabbitCredentials["Password"];
        }

        public ConnectionFactory CreateConnection()
        {
            return new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password
            };
        }
    }
}
