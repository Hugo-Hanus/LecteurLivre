using GBReaderHanusH.Domains.Domains;

namespace Presenter;

public interface IPresenter
{

    public void LoadFile();
    public IList<string> Searching(string search);
    public string ShowDetail(string selectedItem);

    public Library LibraryBook { get; set; }



}