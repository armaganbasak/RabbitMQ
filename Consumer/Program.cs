using System.Net;
using System.Net.Http;
using System.Text;
using Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


while (true)
{

    var factory = new ConnectionFactory { HostName = "localhost", Port = 5672 };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += async (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Convert.ToBase64String(body);
        Console.WriteLine($" [x] Received {message} from RabbitMQ");
        ITcpSender sender = new TcpSender();
        sender.Send(body);
    };
    channel.BasicConsume(queue: "hello",
                         autoAck: true,
                         consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
    

}


