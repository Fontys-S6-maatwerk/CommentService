using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using CommentService.Models;
using CommentService.Repositories;
using CommentService.Mappers;

namespace CommentService.RabbitMQ
{
    public class EventBusReceive
    {
        private IRepository _repository;

        public EventBusReceive(IServiceScopeFactory factory)
        {

            var scope = factory.CreateScope();
            _repository = scope.ServiceProvider.GetRequiredService<IRepository>();

            ReceiveUser();
        }

        public void ReceiveUser()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "auth_logs", type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: "auth_logs",
                              routingKey: "");

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                UserQM response = new UserQM();
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                int dots = message.Split('.').Length - 1;
                try
                {
                    Console.WriteLine("[.] User({0})", message);
                    response = JsonConvert.DeserializeObject<UserQM>(message);

                    _repository.AddUser(CommentMapper.MapToUserDBO(response));
                }
                catch(Exception e)
                {
                    Console.WriteLine("[.] " + e.Message);
                }
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
