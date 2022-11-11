using Presenter.Events;
using Presenter.Notification;
using Presenter.Routes;
using Presenter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Dependencies
{
    public class HomeDependencies : IHomeDependencies
    {
        public IHomeView View { get; set; }

        public IBrowseToViews  Router { get; set; }

    public IShowNotifications NotificationChannels { get; set; }

    public IEventAggregator EventAggregator { get;set; }
}
}
