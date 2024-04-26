// See https://aka.ms/new-console-template for more information
using Optimissa.RabbitMQ;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        _ = RabbitMQReader.Instance.Read(["#"], (ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;
            Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
        });

    }
}