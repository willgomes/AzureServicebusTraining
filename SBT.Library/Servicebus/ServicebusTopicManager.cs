using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;

namespace SBT.Library.Servicebus
{
    public class ServicebusTopicManager
    {
        private NamespaceManager _servicebusNamespaceManager;

        public ServicebusTopicManager( NamespaceManager servicebusNamespaceManager )
        {
            _servicebusNamespaceManager = servicebusNamespaceManager;
        }

        public void CreateTopic(
               string path,
               bool requiresDuplicateDetection = false,
               TimeSpan? duplicateDetectionHistoryTimeWindow = null,
               TimeSpan? defaultMessageTTL = null,
               bool enableFilteringMessagesBeforePublishing = false
           )
        {
            try
            {
                TopicDescription topicDescription = new TopicDescription(path)
                {
                    RequiresDuplicateDetection = requiresDuplicateDetection,
                    DuplicateDetectionHistoryTimeWindow = duplicateDetectionHistoryTimeWindow ?? TimeSpan.FromHours(1),
                    DefaultMessageTimeToLive = defaultMessageTTL ?? TimeSpan.FromHours(1),
                    EnableFilteringMessagesBeforePublishing = enableFilteringMessagesBeforePublishing

                };

                if ( !_servicebusNamespaceManager.TopicExists(path) )
                    _servicebusNamespaceManager.CreateTopic(topicDescription);

            }
            catch ( Exception ex )
            {
                //To Implemenet
            }
        }


        public IEnumerable<TopicDescription> ListTopics()
        {
            return _servicebusNamespaceManager.GetTopics();
        }

        public void EditTopic( string path, string newPath )
        {
            if ( _servicebusNamespaceManager.TopicExists(path) )
                _servicebusNamespaceManager.RenameTopic(path, newPath);
        }

        public void DeleteTopic( string path )
        {
            try
            {
                if ( _servicebusNamespaceManager.TopicExists(path) )
                    _servicebusNamespaceManager.DeleteTopic(path);
            }
            catch ( Exception ex )
            {
                //To Implemenet
            }
        }

        public TopicDescription GetTopicDescription( string topic )
        {
            return _servicebusNamespaceManager.GetTopic(topic);
        }
    }
}
