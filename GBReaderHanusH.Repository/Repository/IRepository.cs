using GBReaderHanusH.Domains.Domains;

namespace GBReaderHanusH.Repository.Repository;

public interface IRepository
{
    public void LoadHistory();
    public void SaveHistory(History history);
    History History { get; set; }
    string Path { get; set; }
}