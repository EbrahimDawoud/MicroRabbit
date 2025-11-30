using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class Receiver
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnectionAsync())
        using (var channel = connection.Result.CreateChannelAsync())
        {
            channel.Result.QueueDeclareAsync("BasicTest", false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(channel.Result);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                await Task.Yield();
            };
            channel.Result.BasicConsumeAsync(queue: "BasicTest",
                                     autoAck: true,
                                     consumer: consumer);
            Console.WriteLine(" Press [enter] to exit the Consumer...");
            Console.ReadLine();
        }
    }
}