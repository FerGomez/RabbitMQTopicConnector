using RabbitMQ.Client;
using System.Text;

namespace Optimissa.RabbitMQ
{
    public class RabbitMQWriter
    {
        public static void Write(string message, string topic)
        {
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

            var routingKey = topic;

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "topic_logs",
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"[x] Sent '{routingKey}':'{message}'");

        }
    }
}