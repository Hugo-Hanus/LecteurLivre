using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.CustomException;
using GBReaderHanusH.Repository.Repository;
using Presenter.Events;
using Presenter.Notification;
using System.Globalization;
using System.IO.Pipes;
using GBReaderHanusH.Repository.Storage;
using Presenter.Dependencies;

namespace Presenter
{
    public class ReadPresenter : IHandle<ReadingBookEventArgs>
    {
        private readonly IReadDependencies _readDependencies=new ReadDependencies();
        private readonly StorageFactory _storageFactory;
        private readonly IRepository _repository;
        private static readonly IDictionary<int, Page> dico = new Dictionary<int, Page>();
        private GameBook _gameBook= new GameBook("","","",new ("",""), dico);
        private History _history=new();

        public ReadPresenter(IReadDependencies readDependencies)
        {
            _readDependencies.View = readDependencies.View;
            _readDependencies.EventAggregator = readDependencies.EventAggregator;
            SubscribeToView();
            _readDependencies.Router = readDependencies.Router;
            _readDependencies.NotificationChannels = readDependencies.NotificationChannels;
            _repository = CreateRepo();
            _storageFactory = CreateFactory();
            
        }

        private void SubscribeToView()
        {
            _readDependencies.View.GoToHome += GoToHome;
            _readDependencies.View.ChoiceMake += GoToPage;
            _readDependencies.View.SaveSession += SaveSession;
            _readDependencies.EventAggregator.Subscribe(this);
        }
        private JsonRepository CreateRepo()
        {
            ISessionMapper mapper = new SessionMapperOne();
            _history = new History();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return new JsonRepository(mapper, _history, path);
        }
        private StorageFactory CreateFactory()
        {
            ConnectionData connectionData = new MySqlConnectionData();
            return new StorageFactory(connectionData); ;
        }
        private void ReadBook()
        {
            
            LoadHistory();
            DateTime formatDateTime = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            if (_history.ContainsIsbn(_gameBook.Isbn))
            {
                _history.ClearPreviousPage();
                SetPage(_history.GetPageOfIsbn(_gameBook.Isbn));
                _history.PreviousPage.Push(_history.GetPageOfIsbn(_gameBook.Isbn));
                _history.UpdateReadingSession(_gameBook.Isbn, _history.GetPageOfIsbn(_gameBook.Isbn),_gameBook.Title, formatDateTime);
            }
            else
            {
                _history.AddSession(_gameBook.Isbn, new Session(formatDateTime, formatDateTime, _gameBook.Title, _gameBook.GetFirstPage()));
                _history.ClearPreviousPage();
                SetPage(_gameBook.GetFirstPage());
                _history.PreviousPage.Push(_gameBook.GetFirstPage());
            }
        }




        private void SetPage(int i)
        {
            if (_gameBook.Pages.ContainsKey(i))
            {
                IList<Choice> choices = _gameBook.Pages[i].Choices;
                _readDependencies.View.PageText = _gameBook.Pages[i].Text;
                _readDependencies.View.PageNumber = i;

                if (_history.PreviousPage.Count>1)
                {
                    AddChoices(-1, "Précédente", false);
                }

                if (choices.Count > 0)
                { 
                    foreach (var choice in choices)
                    {
                            AddChoices(choice.GoTo, choice.Text, false);
                    }
                }
                else
                {
                        AddChoices(-1, "Recommencer la lecture", true);
                }

                _history.UpdateReadingSession(_gameBook.Isbn, i,_gameBook.Title,DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Success, "Session", $"Session mise à jour");
            }
            else
            {
                _readDependencies.View.PageText = "Retourner sur l'acceuil";
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Error, "Page", "Cette Page n'existe pas");
            }
        }


        private void AddChoices(int nbPage, string text, bool finish) => _readDependencies.View.AddChoice(nbPage, text, finish);

        private void LoadHistory()
        {
            try
            {
                _repository.LoadHistory();
                _history = _repository.History;
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Success, "Fichier", "Session Chargé");
            }
            catch (NoPathFindException p)
            {
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Error, "Fichier", p.Message);
            }
            catch (EmptyJsonFileException j)
            {
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Warning, "Fichier", j.Message);
            }
            catch (NotFormatJsonException f)
            {
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Error, "Fichier", f.Message);
            }
        }

        private void GoToPage(object? sender, ChoiceEventArgs args)
        {
            var page = args.Numero;
            if (int.TryParse(page, out int number))
            {
                _readDependencies.View.ClearChoice();
                _history.PreviousPage.Push(number);
                SetPage(number);
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Info, "Choix choisit", $" Vers la page: N°{page}");
            }
            else if (page=="P")
            {
                _readDependencies.View.ClearChoice();
                _history.PreviousPage.Pop();
                SetPage(_history.PreviousPage.Peek());
            }else {
                _readDependencies.View.ClearChoice();
                _history.ClearPreviousPage();
                SetPage(_gameBook.GetFirstPage());
                _history.PreviousPage.Push(_gameBook.GetFirstPage());
                _history.ResetReadingSession(_gameBook.Isbn, DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Info, "Lecture", $"Vous avez choisit de recommencer la lecture");
            }

        }
        private void GoToHome(object? sender,EventArgs e)
        {
            _readDependencies.Router.GoTo("Home");
            var numberPage = _readDependencies.View.PageNumber;
            if (_gameBook.Pages[numberPage].Choices.Count() == 0)
            {
                SetPage(_gameBook.GetFirstPage());
                _readDependencies.View.ClearChoice();
            }
            SaveWhenQuitting();
        }

        private void SaveWhenQuitting()
        {
            try
            {
                _repository.SaveHistory(_history);
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Success, "Fichier", $"Session sauvegardé");
            }
            catch (NotAbleSaveException ex)
            {
                _readDependencies.NotificationChannels.Push(NotificationSeverity.Error, "Fichier", ex.Message);
            }
        }

        private void SaveSession(object? sender, EventArgs e) {
            var numberPage = _readDependencies.View.PageNumber;
            if (_gameBook.Pages[numberPage].Choices.Count() == 0)
            {
                SetPage(_gameBook.GetFirstPage());
            }
            _repository.SaveHistory(_history);
            _history.ClearPreviousPage();
            _readDependencies.View.ClearChoice();
        }
        public void Handle(ReadingBookEventArgs message)
        {
            var isbn = message.isbn;
            _gameBook = LoadGameBook(isbn);
            _gameBook.Pages = LoadPageAndChoice(isbn);
            _readDependencies.View.GameBookIsbn = _gameBook.Isbn;
            _readDependencies.View.GameBookTitle = _gameBook.Title;
            _readDependencies.View.PageText = _gameBook.Pages[1].Text;
            ReadBook();
        }

        private GameBook LoadGameBook(string isbn)
        {

            using var storage = _storageFactory.NewStorage();
            return storage.LoadGameBook(isbn);
        }

        private IDictionary<int, Page> LoadPageAndChoice(string isbn)
        {
            IDictionary<int, Page> pages = new SortedDictionary<int, Page>();

            using (var storage = _storageFactory.NewStorage())
            {
                IList<int> listIdPage = new List<int>();
                listIdPage = storage.LoadIdPageOfGameBook(isbn);
                foreach (var idpage in listIdPage)
                {
                    int numero = storage.LoadNumberPage(idpage);
                    var page = storage.LoadPage(idpage);
                    var choices = storage.LoadChoicesOfAPage(idpage);
                    page.Choices = choices;
                    pages.Add(numero, page);
                }
            }
            return pages;
        }



    }
}
