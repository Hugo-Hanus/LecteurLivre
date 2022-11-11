using GBReaderHanusH.Repository.CustomException;
using GBReaderHanusH.Repository.Storage;
using MySql.Data.MySqlClient;
using Presenter.Dependencies;
using Presenter.Events;
using Presenter.Notification;
using static GBReaderHanusH.Repository.Storage.StorageFactory;

namespace Presenter
{
    public class HomePresenter
    {
       private readonly IHomeDependencies _dependencies=new HomeDependencies();
        private readonly StorageFactory _mySqlStorageFactory;
       
       
        private IList<string> _listView=new List<string>();
        private string _detailGameBook="";

        public HomePresenter(IHomeDependencies homeDependencies)
        {
            _dependencies.View = homeDependencies.View ?? throw new ArgumentNullException(nameof(homeDependencies.View));
            SubscribeToView();
            _dependencies.EventAggregator = homeDependencies.EventAggregator ?? throw new ArgumentNullException(nameof(homeDependencies.EventAggregator));
            _dependencies.Router = homeDependencies.Router ?? throw new ArgumentNullException(nameof(homeDependencies.Router));
            _dependencies.NotificationChannels = homeDependencies.NotificationChannels ?? throw new ArgumentNullException(nameof(homeDependencies.NotificationChannels));
            ConnectionData connectionData = new MySqlConnectionData();
            _mySqlStorageFactory = new StorageFactory(connectionData);
        }

        private void SubscribeToView()
        {
            _dependencies.View.AfterLoad += AfterLoad;
            _dependencies.View.GameBookSelected += GameBookSelected;
            _dependencies.View.SearchGameBook += SearchGameBook;
            _dependencies.View.GoToStats += GoToStats;
            _dependencies.View.ReadingBook += GoToRead;
            _dependencies.View.GameBookDetail = _detailGameBook??"";
        }
        
        private void AfterLoad(object? sender, EventArgs e)
        {
            if (TestConnection())
            {
                LoadResumeBookInfo();
                if (_listView.Count>0)
                {
                    LoadResumeBookSelected(_listView.First()[..13]);
                }
                else
                {
                    _dependencies.NotificationChannels.Push(NotificationSeverity.Warning, "Base de Donnée", "La base de donnée n'a aucun livre publié");
                }
                
            }

        }

        private void GameBookSelected(object? sender, GameBookEventArgs e) => LoadResumeBookSelected(e.Element[..13]);


        private void SearchGameBook(object? sender, GameBookEventArgs args)
        {
            var search = args.Element;
            if (_listView.Count > 0)
            {
                if (search != null)
                {   
                if (!search.Trim().Equals(""))
                {
                    LoadResumeBookInfoSearch(search);
                }
                else
                {
                    LoadResumeBookInfo();
                        _dependencies.NotificationChannels.Push(NotificationSeverity.Warning, "Recherche", $"Recherche vide");
                 }
        
                }
                else
                {
                    _dependencies.NotificationChannels.Push(NotificationSeverity.Warning, "Recherche", $"Recherche impossible sur une liste vide");
                }
            }
            else
            {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Warning, "Recherche", $"Recherche impossible sur une liste vide");
            }
        }

        private void GoToHome(object? sender, EventArgs e) => _dependencies.Router.GoTo("Home");
        private void GoToStats(object? sender, EventArgs e)
        {
            _dependencies.Router.GoTo("Stats");   
        }
        private void GoToRead(object? sender, ReadingBookEventArgs e)
        {
            _dependencies.Router.GoTo("Read");
            _dependencies.EventAggregator.Publish(e);
        }




        private void LoadResumeBookInfo()
        {
            try
            {
                using var storage = _mySqlStorageFactory.NewStorage();
                _listView = storage.SelectResumeGameBookPublish();
                if (_listView.Count > 0)
                {
                    _dependencies.View.Items = _listView;
                }
            
            } catch (MySqlException) {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Error, "Base de Donnée", "Erreur accès à la base de Donnée");
            } catch (ConnectionFailedException e)
            {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Error, "Base de Donnée", e.Message);
            }

        }
        private void LoadResumeBookSelected(string isbnSelected)
        {
            try
            {
                using var storage = _mySqlStorageFactory.NewStorage();
                _dependencies.View.GameBookDetail = storage.SelectResumeGameBookSelected(isbnSelected);
            }
            catch (MySqlException)
            {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Error, "Base de Donnée", "Erreur accès à la base de Donnée");
            }
            catch (ConnectionFailedException e)
            {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Error, "Base de Donnée", e.Message);
            }
        }


        private void LoadResumeBookInfoSearch(string search)
        {
            try
            {
                using var storage = _mySqlStorageFactory.NewStorage();
                _listView = storage.SelectResumeGameBookSearch(search);
                if (_listView.Count > 0)
                {
                    _dependencies.NotificationChannels.Push(NotificationSeverity.Success, "Recherche", $"résultat pour : \"{search}\"");
                    _dependencies.View.Items = _listView;
                }
            }
            catch (MySqlException)
            {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Error, "Base de Donnée", "Erreur accès à la base de Donnée");
            }
            catch (ConnectionFailedException e)
            {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Error, "Base de Donnée", e.Message);
            }

        }
        
        private bool TestConnection()
        {
            try
            {
                using var storage = _mySqlStorageFactory.NewStorage();
                return true;
            }
            catch (UnableToConnectException e)
            {
                _dependencies.NotificationChannels.Push(NotificationSeverity.Error, "Base de Donnée", e.Message);
                return false;
            }
        }
    }
}