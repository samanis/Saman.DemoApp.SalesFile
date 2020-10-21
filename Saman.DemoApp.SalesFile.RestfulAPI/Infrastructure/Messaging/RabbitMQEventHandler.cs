using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure.Interfsaces;
using System;
using System.Text;
using System.Text.Json;

namespace Saman.DemoApp.SalesFile.RestfulAPI.Infrastructure
{
    public class RabbitMQEventHandler : INewUploadedFileEventHandler
    {
        IConfiguration _configuration;
        public RabbitMQEventHandler()
        {
        }

        public RabbitMQEventHandler(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void SendNewFileUploadedEvent(NewFileUploadedEvent newFileUploadedEvent)
        {
            try
            {
                IConfigurationSection configSection = _configuration.GetSection("Messaging"); ;
                var factory = CreateConnectionFactory(configSection);
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    string message = JsonSerializer.Serialize(newFileUploadedEvent);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: configSection.GetSection("RoutingKey").Value, basicProperties: null, body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error in messaging",ex);
            }
        }

        private ConnectionFactory CreateConnectionFactory(IConfigurationSection configSection)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = configSection.GetSection("HostName").Value };
            factory.UserName = configSection.GetSection("UserName").Value;
            factory.Password = configSection.GetSection("Password").Value;
            factory.VirtualHost = configSection.GetSection("VirtualHost").Value;
            return factory;
        }
    }
}
