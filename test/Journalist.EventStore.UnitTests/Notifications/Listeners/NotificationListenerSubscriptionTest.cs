using System;
using System.Threading.Tasks;
using Journalist.EventStore.Connection;
using Journalist.EventStore.Notifications.Listeners;
using Journalist.EventStore.Notifications.Types;
using Journalist.EventStore.UnitTests.Infrastructure.TestData;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace Journalist.EventStore.UnitTests.Notifications.Listeners
{
    public class NotificationListenerSubscriptionTest
    {
        [Theory]
        [AutoMoqData]
        public void Start_NotifyListener(
            [Frozen] Mock<INotificationListener> listenerMock,
            IEventStoreConnection connection,
            NotificationListenerSubscription subscription)
        {
            subscription.Start(connection);

            listenerMock.Verify(self => self.OnSubscriptionStarted(subscription), Times.Once());
        }

        [Theory, AutoMoqData]
        public void Stop_NotifyListener(
            [Frozen] Mock<INotificationListener> listenerMock,
            NotificationListenerSubscription subscription,
            EventStreamUpdated notification)
        {
            subscription.Stop();

            listenerMock.Verify(self => self.OnSubscriptionStopped(), Times.Once());
        }

        [Theory, AutoMoqData]
        public async Task HandleNotificationAsync_WhenSubscriptionStarted_PropagatesNotificationToTheListener(
            [Frozen] Mock<INotificationListener> listenerMock,
            IEventStoreConnection connection,
            NotificationListenerSubscription subscription,
            EventStreamUpdated notification)
        {
            subscription.Start(connection);

            await subscription.HandleNotificationAsync(notification);

            listenerMock.Verify(
                self => self.On(notification),
                Times.Once());
        }

        [Theory, AutoMoqData]
        public async Task HandleNotificationAsync_WhenSubscriptionDidNotStart_NotPropagatesNotificationToTheListener(
            [Frozen] Mock<INotificationListener> listenerMock,
            NotificationListenerSubscription subscription,
            EventStreamUpdated notification)
        {
            await subscription.HandleNotificationAsync(notification);

            listenerMock.Verify(
                self => self.On(notification),
                Times.Never());
        }

        [Theory, AutoMoqData]
        public async Task HandleNotificationAsync__WhenSubscriptionStoped_PropagateNotificationToTheListener(
            [Frozen] Mock<INotificationListener> listenerMock,
            IEventStoreConnection connection,
            NotificationListenerSubscription subscription,
            EventStreamUpdated notification)
        {
            subscription.Start(connection);
            await subscription.HandleNotificationAsync(notification);

            subscription.Stop();
            await subscription.HandleNotificationAsync(notification);

            listenerMock.Verify(
                self => self.On(notification),
                Times.Once());
        }

        [Theory, AutoMoqData]
        public void ConnectionProperty_WhenSubscriptionWasStarted_ReturnsConnection(
            IEventStoreConnection connection,
            NotificationListenerSubscription subscription)
        {
            subscription.Start(connection);

            Assert.Same(subscription.Connection, connection);
        }

        [Theory, AutoMoqData]
        public void ConnectionProperty_WhenSubscriptionWasNotStarted_Throws(
            IEventStoreConnection connection,
            NotificationListenerSubscription subscription)
        {
            Assert.Throws<InvalidOperationException>(() => subscription.Connection);
        }

        [Theory, AutoMoqData]
        public void ConnectionProperty_WhenSubscriptionWasStopped_Throws(
            IEventStoreConnection connection,
            NotificationListenerSubscription subscription)
        {
            subscription.Start(connection);
            subscription.Stop();

            Assert.Throws<InvalidOperationException>(() => subscription.Connection);
        }
    }
}
