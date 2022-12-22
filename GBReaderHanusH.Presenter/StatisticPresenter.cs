using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.CustomException;
using GBReaderHanusH.Repository.Repository;
using Presenter.Events;
using Presenter.Notification;
using Presenter.Routes;
using Presenter.Views;

namespace Presenter
{
    public class StatisticPresenter
    {
        private readonly IStatisticView _view;
        private readonly IBrowseToViews _router;
        private readonly IShowNotifications _notificationChannels;
        private readonly IRepository _repository;
        private IList<string> _listView = new List<string>();

        public StatisticPresenter(IStatisticView view, IBrowseToViews router, IShowNotifications notificationChannel)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            SubscribeToView();
            _router = router ?? throw new ArgumentNullException(nameof(router));
            _repository = InitRepo();
            _notificationChannels = notificationChannel ?? throw new ArgumentNullException(nameof(notificationChannel));
            
        }
        private void SubscribeToView()
        {
            _view.GoToHome += GoToHome;
            _view.LoadHistoryFile += LoadHistoryFile;
        }
        private static JsonRepository InitRepo()
        {
            ISessionMapper mapper = new SessionMapperOne();
            History history = new ();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return new JsonRepository(mapper, history, path);
        }


        private void LoadStats()
        {
            LoadHistory();
            var history = _repository.History;
            _listView = new List<string>();
            SetElemToView(history);
        }

        private void SetElemToView(History history)
        {
            foreach (var session in history.SessionList)
            {
                _listView.Add(session.Value.ToDetail());
            }
            _view.Items = _listView;
            _view.NumberReading = _listView.Count.ToString();
        }

        private void LoadHistory()
        {
            try
            {
                _repository.LoadHistory();
            }
            catch (NoPathFindException p)
            {
                _notificationChannels.Push(NotificationSeverity.Error, "Fichier", p.Message);
            }
            catch (EmptyJsonFileException j)
            {
                _notificationChannels.Push(NotificationSeverity.Warning, "Fichier", j.Message);
            }
            catch (NotFormatJsonException f)
            {
                _notificationChannels.Push(NotificationSeverity.Error, "Fichier", f.Message);
            }
        }
        private void GoToHome(object? sender, EventArgs e) => _router.GoTo("Home");
        private void LoadHistoryFile(object? sender, EventArgs e) => LoadStats();
    }
}