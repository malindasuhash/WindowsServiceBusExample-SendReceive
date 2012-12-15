using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceConstants
{
    /// <summary>
    /// This class builds the connection string required to access the Service Bus.
    /// </summary>
    public class ConnectionBuilder
    {
        private static readonly string ServiceBusEndPont = "sb://Niranga/ServiceBusDefaultNamespace";

        private static readonly string SecureTokenServiceEndPoint = "https://Niranga:9355/ServiceBusDefaultNamespace";

        private static int HttpPort = 9355;

        private static int TcpPort = 9354;

        public string ConnectionString { get; private set; }

        public void Build()
        {
            var connectionBuilder = new ServiceBusConnectionStringBuilder();
            connectionBuilder.ManagementPort = HttpPort;
            connectionBuilder.RuntimePort = TcpPort;
            
            // Service Bus end-point
            connectionBuilder.Endpoints.Add(new Uri(ServiceBusEndPont));

            // STS End-point
            connectionBuilder.StsEndpoints.Add(new Uri(SecureTokenServiceEndPoint));

            ConnectionString = connectionBuilder.ToString();
        }
    }
}
