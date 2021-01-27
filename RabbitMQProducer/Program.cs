using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "demo", false, false, false, null);
                    channel.QueueDeclare(queue: "demo2", false, false, false, null);

                    for (int i = 0; i < 100; i++)
                    {
                        string message = $"Hello RabbitMQ {i + 1}";

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: string.Empty, routingKey: "demo", basicProperties: null, body: body);
                        channel.BasicPublish(exchange: string.Empty, routingKey: "demo2", basicProperties: null, body: body);
                    }
                }
            }
        }
    }
}
