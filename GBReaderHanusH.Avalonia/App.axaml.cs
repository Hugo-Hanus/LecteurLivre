using System;
using System.Diagnostics.Metrics;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderHanusH.Avalonia.Pages;
using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.Repository;
using Presenter;
using Presenter.Dependencies;
using Presenter.Events;


namespace GBReaderHanusH.Avalonia
{
    public partial class App : Application
    {
        private MainWindow _mainWindow =new();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _mainWindow = new MainWindow();
                desktop.MainWindow = _mainWindow;
                InjectViewDependency();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InjectViewDependency()
        {
            IEventAggregator eventAggregator = new EventAggregator();
           

            /*View*/
            var homePage = new HomePage();
            var readPage = new ReadPage();
            var statisticPage = new StatisticPage();
            IReadDependencies readDependencies = new ReadDependencies
            {
                View = readPage,
                Router = _mainWindow,
                NotificationChannels = _mainWindow,
                EventAggregator = eventAggregator
            };
            IHomeDependencies homeDependencies = new HomeDependencies
            {
                View = homePage,
                Router = _mainWindow,
                NotificationChannels = _mainWindow,
                EventAggregator = eventAggregator
            };

            /*Presenter*/

            var homePresenter = new HomePresenter(homeDependencies);
            
            var readPresenter = new ReadPresenter(readDependencies);
           
            var statisticPresenter = new StatisticPresenter(statisticPage, _mainWindow, _mainWindow);
            
            _mainWindow.RegisterPage("Home",homePage);
            _mainWindow.RegisterPage("Read",readPage);
            _mainWindow.RegisterPage("Stats",statisticPage);

        }
    }
}