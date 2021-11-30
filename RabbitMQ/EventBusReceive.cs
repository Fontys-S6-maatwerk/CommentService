﻿using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using CommentService.Models;
using CommentService.Repositories;

namespace CommentService.RabbitMQ
{
    public class EventBusReceive
    {
        private IConnection connection;
        private IModel channel;

        //TODO check if right db class
        private CommentContext _context;

        public EventBusReceive(IServiceScopeFactory factory)
        {

            var scope = factory.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<CommentContext>();

            ReceiveUser();
        }

        public void ReceiveUser()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: "update_user_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                User response = new User();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                int dots = message.Split('.').Length - 1;

                //Do something with the message.
                try
                {
                    Console.WriteLine(" [.] User({0})", message);
                    response = JsonConvert.DeserializeObject<User>(message);

                    //TODO save changes in database
                    //_context.User.Add(response);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    response = null;
                }

                Console.WriteLine(" [x] Done " + response.FirstName);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: "update_user_queue", autoAck: false, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");

        }
    }
}
