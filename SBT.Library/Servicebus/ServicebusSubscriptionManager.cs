using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;

namespace SBT.Library.Servicebus
{
    public class ServicebusSubscriptionManager
    {
        private NamespaceManager _servicebusNamespaceManager;

        public ServicebusSubscriptionManager( NamespaceManager servicebusNamespaceManager )
        {
            _servicebusNamespaceManager = servicebusNamespaceManager;
        }

        public void CreateSubscription(
               string path,
               string topicPath,
               TimeSpan? defaultMessageTTL = null,
               TimeSpan? autoDeleteOnIdle = null,
               int maxDeliveryCount = 10
           )
        {
            try
            {
                SubscriptionDescription topicDescription = new SubscriptionDescription(topicPath, path)
                {
                    DefaultMessageTimeToLive = defaultMessageTTL ?? TimeSpan.FromHours(1),
                    AutoDeleteOnIdle = autoDeleteOnIdle ?? TimeSpan.FromHours(5),
                    MaxDeliveryCount = maxDeliveryCount
                };

                if ( !_servicebusNamespaceManager.SubscriptionExists(topicPath, path) )
                    _servicebusNamespaceManager.CreateSubscription(topicDescription);

            }
            catch ( Exception ex )
            {
                //To Implemenet
            }
        }

        public void CreateSubscription(
              string path,
              string topicPath,
              Filter filter,
              TimeSpan? defaultMessageTTL = null,
              TimeSpan? autoDeleteOnIdle = null,
              int maxDeliveryCount = 10
          )
        {
            try
            {
                SubscriptionDescription topicDescription = new SubscriptionDescription(topicPath, path)
                {
                    DefaultMessageTimeToLive = defaultMessageTTL ?? TimeSpan.FromHours(1),
                    AutoDeleteOnIdle = autoDeleteOnIdle ?? TimeSpan.FromHours(5),
                    MaxDeliveryCount = maxDeliveryCount
                };

                if ( !_servicebusNamespaceManager.SubscriptionExists(topicPath, path) )
                    _servicebusNamespaceManager.CreateSubscription(topicDescription, filter);

            }
            catch ( Exception ex )
            {
                //To Implemenet
            }
        }

        public IEnumerable<SubscriptionDescription> ListSubscriptions( string topicPath )
        {
            return _servicebusNamespaceManager.GetSubscriptions(topicPath);
        }

        public void DeleteSubscription( string topicPath, string path )
        {
            try
            {
                if ( _servicebusNamespaceManager.SubscriptionExists(topicPath, path) )
                    _servicebusNamespaceManager.DeleteSubscription(topicPath, path);
            }
            catch ( Exception ex )
            {
                //To Implemenet
            }
        }

        public SubscriptionDescription GetSubscriptionDescription( string topicPath, string topic )
        {
            return _servicebusNamespaceManager.GetSubscription(topicPath, topic);
        }
    }
}
