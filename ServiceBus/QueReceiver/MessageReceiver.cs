using Microsoft.ServiceBus.Messaging;
using ServiceConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueReceiver
{
    /// <summary>
    /// This class is responsible for receiving messages from Service Bus.
    /// </summary>
    public class MessageReceiver
    {
        private string _connectionString;
        private QueueClient _client;

        public string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    var connectionBuilder = new ConnectionBuilder();
                    connectionBuilder.Build();

                    _connectionString = connectionBuilder.ConnectionString;
                }

                return _connectionString;
            }
        }

        public string ReceivedMessage()
        {
            var factory = MessagingFactory.CreateFromConnectionString(ConnectionString);
            _client = factory.CreateQueueClient(ServiceProperties.QueueName);

            var message = _client.Receive();

            return message.GetBody<string>();
        }
    }
}
