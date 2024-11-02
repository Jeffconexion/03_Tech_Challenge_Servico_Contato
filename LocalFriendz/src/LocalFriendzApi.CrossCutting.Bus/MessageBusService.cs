using LocalFriendzApi.CrossCutting.Bus.Contracts;
using RabbitMQ.Client;

namespace LocalFriendzApi.CrossCutting.Bus
{
    public class MessageBusService : IMessageBusService
    {

        private readonly ConnectionFactory _factory;

        public MessageBusService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        public void Publish(string queue, byte[] message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: queue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: properties,
                        body: message);
                }
            }
        }
    }
}
