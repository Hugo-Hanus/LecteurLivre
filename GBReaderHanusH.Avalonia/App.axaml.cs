using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.Repository;
using Presenter.mvp;

namespace GBReaderHanusH.Avalonia
{
    public partial class App : Application
    {
        private MainWindow _mainWindow;

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
            IMapper mapper = new MapperOne();
            Library library = new Library();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+@"\ue36\190533.json";
            IRepository repository = new JsonRepository(mapper, library, path);
            new MainSupervisor(_mainWindow,repository,library);
        }
    }
}