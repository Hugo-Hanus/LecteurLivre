using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Repository.Repository;

namespace Presenter;

public class Presenter :IPresenter
{
    string PATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+@"\ue36\190533.json";
    private IRepository _repository;
    public Presenter(IRepository repository,Library library)
    {
        LibraryBook = library;
        _repository = _repository;

    }

    public Library LibraryBook
    {
        get;
        set;
    }


    public void LoadFile()
    {
        throw new NotImplementedException();
    }

    public IList<string> Searching(string search)
    {
        throw new NotImplementedException();
    }

    public string ShowDetail(string selectedItem)
    {
        throw new NotImplementedException();
    }
}