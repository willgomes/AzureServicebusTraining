using Microsoft.ServiceBus.Messaging;
using System.Configuration;

namespace SBT.Library.ServiceBusClients
{
    public class ServicebusSubscriptionClient
    {
        public SubscriptionClient subscriptionClient( string topicPath, string subscriptionName )
        {
            return SubscriptionClient.CreateFromConnectionString(ConfigurationManager.AppSettings[ "ServicebusTraining" ], topicPath, subscriptionName);
        }
    }
}
