using RabbitMQ.Client;
using System.Text;
using System;
using Send;

public class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {

            Console.WriteLine("\nGöndermek istediğiniz mesajı yazınız: ");
            string messageFromUser = Console.ReadLine();

            string key = "12345678901234567890123456789a!!"; // 32 karakter uzunluğunda bir anahtar gerekiyor
            byte[] encryptedBytes = Encryption.EncryptAes(messageFromUser, key);
            string encryptedText = Convert.ToBase64String(encryptedBytes);

            Console.WriteLine("Gönderilen veri şifrelenmiş hali : ",encryptedText);
            


            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = Convert.ToBase64String(encryptedBytes);
            var body = encryptedBytes;

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent to the RabbitMQ {message}");


            Console.WriteLine(" Press 'g' to continue. Press 'h' to stop.");

            //DateTime startTime = DateTime.Now;
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.KeyChar == 'g')
            {
                continue;
            }
            else if (keyInfo.KeyChar == 'h')
            {
                Console.WriteLine("Data transfer is stopped.");
                break;
            }

        }
    }
}