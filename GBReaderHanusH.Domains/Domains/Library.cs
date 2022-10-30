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

    public IList<string> ToListView()
    {
        IList<string> list = new List<string>();
        foreach(var item in _libraryList) {
            list.Add(item.Value.ToString());
        }
        return list;
    }

    public int SizeOfLibrary() => _libraryList.Count;

}
    