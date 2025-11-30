using RabbitMQ.Client;
using System.Text;

public class Sender
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnectionAsync())

        using (var channel = connection.Result.CreateChannelAsync())
        {
            channel.Result.QueueDeclareAsync("BasicTest",false,  false, false,null);
            string message = "Getting started with .net Core Rqbbit MQ";
            var body = Encoding.UTF8.GetBytes(message);
            channel.Result.BasicPublishAsync("", "BasicTest", false,  body);
            Console.WriteLine(" [x] Sent {0}", message);

        }
        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();

    }
}