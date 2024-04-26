using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace Optimissa.RabbitMQ
{
    public class RabbitMQReader
    {
        private static readonly RabbitMQReader _reader = new RabbitMQReader();
        private ConnectionFactory _factory;
        private RabbitConnection _connection;

        private RabbitMQReader()
        {
            _connection = new RabbitConnection();
            _factory = _connection.CreateConnection();
        }

        public static RabbitMQReader Instance
        {
            get
            {
                return _reader;
            }
        }


        public async Task Read(string[] topics, Action<BasicDeliverEventArgs> act)
        {

            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
            var queueName = channel.QueueDeclare().QueueName;

            if (topics.Length < 1 || topics == null)
                return;


            foreach (var bindingKey in topics)
            {
                channel.QueueBind(queue: queueName,
                                  exchange: "topic_logs",
                                  routingKey: bindingKey);
            }

            var consumer = new EventingBasicConsumer(channel);


            consumer.Received += (_, ea) => act(ea);

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);


            Console.Read();
        }
    }
}