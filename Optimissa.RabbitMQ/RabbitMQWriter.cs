using RabbitMQ.Client;
using System.Text;

namespace Optimissa.RabbitMQ
{
    public class RabbitMQWriter
    {
        RabbitConnection _connection;
        ConnectionFactory _factory;


        public RabbitMQWriter(RabbitConnection rabbitConnection)
        {
            _connection = rabbitConnection;
            _factory = _connection.CreateConnection();
        }

        public void Write(string message, string topic)
        {
            using var connection = _factory.CreateConnection();
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