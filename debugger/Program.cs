using Optimissa.RabbitMQ;


// See https://aka.ms/new-console-template for more information


RabbitConnection connection = new RabbitConnection();

Console.WriteLine("Type a topic in which to send your message:");
string topic = Console.ReadLine().ToString();
Console.Clear();

RabbitMQWriter writer = new RabbitMQWriter();

while (true)
{
    Console.Clear();
    Console.WriteLine("[x] Please type a message to send");
    var message = Console.ReadLine();

    writer.Write(message, topic);

}