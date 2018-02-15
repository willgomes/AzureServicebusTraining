using Microsoft.ServiceBus.Messaging;
using System.Configuration;

namespace SBT.Library.ServiceBusClients
{
    public class ServicebusTopicClient
    {
        public TopicClient TopicClientSb(string path)
        {
            return TopicClient.CreateFromConnectionString(ConfigurationManager.AppSettings[ "ServicebusTraining" ], path);
        }

    }
}
