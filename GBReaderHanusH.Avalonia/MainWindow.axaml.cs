using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Presenter.Notification;
using Presenter.Routes;


namespace GBReaderHanusH.Avalonia
{


    /*
     ECOUTE EVENT ABONNER WINDOWS CLOSING QUE DANS LA VUE SINON !!!! AVALONIA DANS LE PRESENTER
     
     */
    public partial class MainWindow : Window, IBrowseToViews, IShowNotifications
    {
        private readonly IDictionary<string, UserControl> _pages = new Dictionary<string, UserControl>();
        private readonly WindowNotificationManager _notificationManager;

        public MainWindow()
        {
            InitializeComponent();
            _notificationManager = new WindowNotificationManager(this)
            {
                Position = NotificationPosition.BottomRight
            };
        }

        internal void RegisterPage(string pageName, UserControl page)
        {
            _pages[pageName] = page;
            if (Content == null)
            {
                Content = page;
            }
        }

        public void GoTo(string namePage)
        {
            Content = _pages[namePage];
        }
       

        public void Push(NotificationSeverity severity, string title, string message)
        {
            var notification = new Notification(title, message, severity switch
            {
                NotificationSeverity.Info => NotificationType.Information,
                NotificationSeverity.Warning => NotificationType.Warning,
                NotificationSeverity.Success => NotificationType.Success,
                _ => NotificationType.Error,
            });

            if (this.IsVisible)
            {
                _notificationManager.Show(notification);
            }
        }
    }
}