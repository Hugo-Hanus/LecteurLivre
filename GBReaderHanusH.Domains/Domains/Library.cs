namespace GBReaderHanusH.Domains.Domains;

public class Library
{
    private IDictionary<string, GameBook> _libraryList;
    public Library()
    {
        _libraryList = new SortedDictionary<string, GameBook>();
    }

    public void SetLibrary(IDictionary<string, GameBook> list) => _libraryList = list;
    public IDictionary<string, GameBook> GetLibrary() => _libraryList;

    public int SizeOfLibrary() => _libraryList.Count;

}
    