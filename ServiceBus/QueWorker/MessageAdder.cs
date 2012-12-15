using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueWorker
{
    /// <summary>
    /// This class adds messages to the Service Bus.
    /// </summary> 
    public class MessageSender
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

        public void CreateQueue()
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);

            if (namespaceManager.QueueExists(ServiceProperties.QueueName))
            {
                namespaceManager.DeleteQueue(ServiceProperties.QueueName);
            }

            namespaceManager.CreateQueue(ServiceProperties.QueueName);
        }

        public void CreateSender()
        {
            var factory = MessagingFactory.CreateFromConnectionString(ConnectionString);
            _client = factory.CreateQueueClient(ServiceProperties.QueueName);
        }

        public void Send(string msg)
        {
            var message = new BrokeredMessage(msg);
            _client.Send(message);
        }
    }
}
