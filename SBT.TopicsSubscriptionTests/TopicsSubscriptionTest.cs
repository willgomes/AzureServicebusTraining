using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBT.Library.Contract;
using SBT.Library.Servicebus;
using SBT.Library.ServiceBusClients;
using System.Linq;

namespace SBT.TopicsSubscriptionTests
{
    [TestClass]
    public class TopicsSubscriptionTest
    {
        [TestMethod]
        public void AddingNewTopic()
        {
            var TopicManager = new ServicebusTopicManager(ServicebusManagerCore.NamespaceManagerSB());
            TopicManager.CreateTopic("Club");
            TopicManager.CreateTopic("Console");

            Assert.Inconclusive("4Fun :)");
        }

        [TestMethod]
        public void AddingNewSubscriptions()
        {
            var subscriptionManager = new ServicebusSubscriptionManager(ServicebusManagerCore.NamespaceManagerSB());
            subscriptionManager.CreateSubscription(path: "TeenCostumers", topicPath: "Club", filter: new SqlFilter("Age >= 10 AND Age < 18"));
            subscriptionManager.CreateSubscription(path: "AdultCostumers", topicPath: "Club", filter: new SqlFilter("Age >= 18 AND Age < 50"));
            subscriptionManager.CreateSubscription(path: "LoyalCostumers", topicPath: "Club", filter: new SqlFilter("Loyal = true"));

            subscriptionManager.CreateSubscription(path: "XboxCostumers", topicPath: "Console", filter: new SqlFilter("Device = 'Xbox' "));
            subscriptionManager.CreateSubscription(path: "PSCostumers", topicPath: "Console", filter: new SqlFilter("Device = 'PS' "));
            subscriptionManager.CreateSubscription(path: "PCCostumers", topicPath: "Console", filter: new SqlFilter("Device = 'PC' "));


            Assert.Inconclusive("4Fun :)");
        }

        [TestMethod]
        public void AddingNewMessages()
        {
            ServicebusTopicClient topicClient = new ServicebusTopicClient();

            List<Members> members = new List<Members>();
            members.Add(new Members
            {
                Age = 15,
                Name = "Maria",
                Loyal = false
            });
            members.Add(new Members
            {
                Age = 12,
                Name = "José",
                Loyal = false
            });
            members.Add(new Members
            {
                Age = 10,
                Name = "João",
                Loyal = false
            });
            members.Add(new Members
            {
                Age = 25,
                Name = "Shazam",
                Loyal = false
            });
            members.Add(new Members
            {
                Age = 50,
                Name = "Alberto",
                Loyal = true
            });

            foreach ( var member in members )
            {
                BrokeredMessage brokeredMessage = new BrokeredMessage();
                brokeredMessage.Properties.Add("Name", member.Name);
                brokeredMessage.Properties.Add("Age", member.Age);
                brokeredMessage.Properties.Add("Loyal", member.Loyal);

                topicClient.TopicClientSb("Club").Send(brokeredMessage);
            }

            List<string> Devices = new List<string> { "Xbox", "PC", "PS" };
            Random rnd = new Random();

            for ( var i = 0; i < 20; i++ )
            {
                BrokeredMessage brokeredMessage = new BrokeredMessage();
                brokeredMessage.Properties.Add("Device", Devices[ rnd.Next(Devices.Count) ]);
                topicClient.TopicClientSb("Console").Send(brokeredMessage);
            }

            Assert.Inconclusive("4Fun :)");
        }

        [TestMethod]
        public void ListTopicsIsNotEmpty()
        {
            ServicebusTopicManager topicManager = new ServicebusTopicManager(ServicebusManagerCore.NamespaceManagerSB());
            IEnumerable<TopicDescription> topics = topicManager.ListTopics();

            Assert.IsTrue(topics.Any());
        }

    }
}
