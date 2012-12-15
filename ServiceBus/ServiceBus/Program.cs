using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    class Program
    {
        static string ServerFQDN;
        static int HttpPort = 9355;
        static int TcpPort = 9354;
        static string ServiceNamespace = "ServiceBusDefaultNamespace";

        static void Main(string[] args)
        {
            var connBuilder = new ServiceBusConnectionStringBuilder();
            connBuilder.ManagementPort = HttpPort;
            connBuilder.RuntimePort = TcpPort;
            connBuilder.Endpoints.Add(new Uri("sb://Niranga/ServiceBusDefaultNamespace"));

            connBuilder.StsEndpoints.Add(new Uri("https://Niranga:9355/ServiceBusDefaultNamespace"));

            var messagingFactory = MessagingFactory.CreateFromConnectionString(connBuilder.ToString());
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connBuilder.ToString());

            if (namespaceManager == null)
            {
                Console.WriteLine("Error");
                return;
            }

            string queueName = "ServiceBusQueueSample";

            if (namespaceManager.QueueExists(queueName))
            {
                namespaceManager.DeleteQueue(queueName);
            }

            namespaceManager.CreateQueue(queueName);

            var queueClient = messagingFactory.CreateQueueClient(queueName);

            // Send
            var brokeredMessage = new BrokeredMessage("Hello");
            queueClient.Send(brokeredMessage);


            // Receive
            var receiveMessage = queueClient.Receive(TimeSpan.FromSeconds(5));

            if (receiveMessage != null)
            {
                Console.WriteLine("Received '{0}'", receiveMessage.GetBody<string>());
                receiveMessage.Complete();
            }

            if (messagingFactory != null)
            {
                messagingFactory.Close();
            }
        }
    }
}
