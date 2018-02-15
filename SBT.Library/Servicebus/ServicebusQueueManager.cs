using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;

namespace SBT.Library.Servicebus
{
    public class ServicebusQueueManager
    {
        private NamespaceManager _servicebusNamespaceManager;

        public ServicebusQueueManager( NamespaceManager servicebusNamespaceManager )
        {
            _servicebusNamespaceManager = servicebusNamespaceManager;
        }

        public void CreateQueue(
                string path,
                bool requiresDuplicateDetection = false,
                TimeSpan? duplicateDetectionHistoryTimeWindow = null,
                bool requiresSession = false,
                int maxDeliveryCount = 10,
                TimeSpan? defaultMessageTTL = null,
                bool enableDeadLetteringOnMessageExpiration = false
            )
        {
            try
            {
                QueueDescription queueDescription = new QueueDescription(path)
                {
                    RequiresDuplicateDetection = requiresDuplicateDetection,
                    DuplicateDetectionHistoryTimeWindow = duplicateDetectionHistoryTimeWindow ?? TimeSpan.FromHours(1),
                    RequiresSession = requiresSession,
                    MaxDeliveryCount = maxDeliveryCount,
                    DefaultMessageTimeToLive = defaultMessageTTL ?? TimeSpan.FromHours(1),
                    EnableDeadLetteringOnMessageExpiration = enableDeadLetteringOnMessageExpiration
                };

                if ( !_servicebusNamespaceManager.QueueExists(path) )
                    _servicebusNamespaceManager.CreateQueue(queueDescription);

            }
            catch ( Exception ex )
            {
                new NotImplementedException();
            }
        }

        public void DeleteQueue( string path )
        {
            try
            {
                if ( _servicebusNamespaceManager.QueueExists(path) )
                    _servicebusNamespaceManager.DeleteQueue(path);
            }
            catch ( Exception ex )
            {
                new NotImplementedException();
            }
        }

        public IEnumerable<QueueDescription> ListQueues()
        {
            return _servicebusNamespaceManager.GetQueues();
        }

        public void EditQueue( string path, string newPath )
        {
            if ( _servicebusNamespaceManager.QueueExists(path) )
                _servicebusNamespaceManager.RenameQueue(path, newPath);
        }

        public QueueDescription GetQueue( string queue )
        {
            return _servicebusNamespaceManager.GetQueue(queue);
        }

    }
}
