using Microsoft.ServiceBus;
using System.Configuration;

namespace SBT.Library.Servicebus
{
    public static class ServicebusManagerCore
    {
        public static NamespaceManager NamespaceManagerSB()
        {
            return NamespaceManager.CreateFromConnectionString(ConfigurationManager.AppSettings[ "ServicebusTraining" ]);
        }

    }
}
