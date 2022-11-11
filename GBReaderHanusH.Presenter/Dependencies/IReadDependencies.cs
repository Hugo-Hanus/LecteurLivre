﻿using Presenter.Events;
using Presenter.Notification;
using Presenter.Routes;
using Presenter.Views;


namespace Presenter.Dependencies
{
    public interface IReadDependencies
    {
        IReadView View { get; set; }
        IBrowseToViews Router { get; set; }
        IShowNotifications NotificationChannels { get; set; }
        IEventAggregator EventAggregator { get; set; }
    }
}
