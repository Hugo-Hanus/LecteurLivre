using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Repository.Repository;
using Presenter.Events;

namespace Presenter.mvp
{
    
    public class MainSupervisor
    {
        private readonly IMainView _view;
        private readonly IRepository _repository;
        private readonly Library _library;
        private IList<string> _listView;
        private string _detailGameBook;

        public MainSupervisor(IMainView view,IRepository repository,Library library)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository;
            InitializeListFromJson();
            _library = _repository.Library;
            _listView = _library.ToListView();
            _view.Items = _listView;
            _view.GameBookDetail = _detailGameBook;
            _view.GamebookSelected += SelectGameBook;
            _view.SearchGameBook += SearchGameBook;
            
        }


        private void InitializeListFromJson()
        {
            if (_repository.LoadBook())
            {
                if (_repository.Library.SizeOfLibrary() > 0)
                {
                    _view.PushMessage("#306B34","Fichier Chargé");
                    var first = _repository.Library.GetLibrary().First().Value;
                    _view.GetContentView(true);
                    _view.GameBookDetail = first.ToDetail();
                }
                else
                {
                    _view.PushMessage("#CF5C36","Erreur lecture du fichier");
                }
            }
            else
            {
                _view.PushMessage("#B6174B","Erreur pas de fichier trouvé");
            }
        }


        public void SelectGameBook(object sender, GameBookEventArgs args)
        {
            var isbn = args.Element[..13];
            var dictionary = _library.GetLibrary();
            _view.GameBookDetail = dictionary[isbn].ToDetail();
        }
        public void SearchGameBook(object sender, GameBookEventArgs args)
        {
            var search =args.Element;
            var dictionary = _library.GetLibrary();
            if (!search.Equals("")) { 
                IList<string> searchList = SearchInLibrary(dictionary, search);
                _view.Items = searchList;
            }
            else
            {
                _view.Items = _library.ToListView();
            }
            _view.GameBookDetail = "";
            _view.PushMessage("#000000", "Résultat pour: " + search);
        }

        private static IList<string> SearchInLibrary(IDictionary<string, GameBook> dictionary, string search)
        {
            IList<string> searchList = new List<string>();
            foreach (var keyValue in dictionary)
            {
                if (keyValue.Key.Equals(search))
                {
                    searchList.Add(keyValue.Value.ToString());
                }

                if (keyValue.Value.Title.Contains(search))
                {
                    searchList.Add(keyValue.Value.ToString());
                }
            }
            return searchList;
        }
    }
}