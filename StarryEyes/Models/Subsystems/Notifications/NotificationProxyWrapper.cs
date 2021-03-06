﻿using System;
using JetBrains.Annotations;
using StarryEyes.Anomaly;
using StarryEyes.Anomaly.TwitterApi.DataModels;
using StarryEyes.Feather.Proxies;

namespace StarryEyes.Models.Subsystems.Notifications
{
    public sealed class NotificationProxyWrapper : INotificationSink
    {
        private readonly INotificationProxy _proxy;

        internal INotificationSink Next { get; set; }

        public NotificationProxyWrapper([NotNull] INotificationProxy proxy)
        {
            if (proxy == null) throw new ArgumentNullException(nameof(proxy));
            _proxy = proxy;
        }

        public void NotifyReceived(TwitterStatus status)
        {
            if (!_proxy.NotifyReceived(status))
            {
                Next?.NotifyReceived(status);
            }
        }

        public void NotifyNewArrival(TwitterStatus status, NotificationType type, string explicitSoundSource)
        {
            if (!_proxy.NotifyNewArrival(status, type, explicitSoundSource))
            {
                Next?.NotifyNewArrival(status, type, explicitSoundSource);
            }
        }

        public void NotifyFollowed(TwitterUser source, TwitterUser target)
        {
            if (!_proxy.NotifyFollowed(source, target))
            {
                Next?.NotifyFollowed(source, target);
            }
        }

        public void NotifyUnfollowed(TwitterUser source, TwitterUser target)
        {
            if (!_proxy.NotifyUnfollowed(source, target))
            {
                Next?.NotifyUnfollowed(source, target);
            }
        }

        public void NotifyBlocked(TwitterUser source, TwitterUser target)
        {
            if (!_proxy.NotifyBlocked(source, target))
            {
                Next?.NotifyBlocked(source, target);
            }
        }

        public void NotifyUnblocked(TwitterUser source, TwitterUser target)
        {
            if (!_proxy.NotifyUnblocked(source, target))
            {
                Next?.NotifyUnblocked(source, target);
            }
        }

        public void NotifyMuted(TwitterUser source, TwitterUser target)
        {
            if (!_proxy.NotifyMuted(source, target))
            {
                Next?.NotifyMuted(source, target);
            }
        }

        public void NotifyUnmuted(TwitterUser source, TwitterUser target)
        {
            if (!_proxy.NotifyUnmuted(source, target))
            {
                Next?.NotifyUnmuted(source, target);
            }
        }

        public void NotifyFavorited(TwitterUser source, TwitterStatus status)
        {
            if (!_proxy.NotifyFavorited(source, status))
            {
                Next?.NotifyFavorited(source, status);
            }
        }

        public void NotifyUnfavorited(TwitterUser source, TwitterStatus status)
        {
            if (!_proxy.NotifyUnfavorited(source, status))
            {
                Next?.NotifyUnfavorited(source, status);
            }
        }

        public void NotifyRetweeted(TwitterUser source, TwitterStatus original, TwitterStatus retweet)
        {
            if (!_proxy.NotifyRetweeted(source, original, retweet))
            {
                Next?.NotifyRetweeted(source, original, retweet);
            }
        }

        public void NotifyDeleted(long statusId, TwitterStatus deleted)
        {
            if (!_proxy.NotifyDeleted(statusId, deleted))
            {
                Next?.NotifyDeleted(statusId, deleted);
            }
        }

        public void NotifyLimitationInfoGot(IOAuthCredential account, int trackLimit)
        {
            if (!_proxy.NotifyLimitationInfoGot(account, trackLimit))
            {
                Next?.NotifyLimitationInfoGot(account, trackLimit);
            }
        }

        public void NotifyUserUpdated(TwitterUser source)
        {
            if (!_proxy.NotifyUserUpdated(source))
            {
                Next?.NotifyUserUpdated(source);
            }
        }
    }
}
