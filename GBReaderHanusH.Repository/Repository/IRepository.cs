using GBReaderHanusH.Domains.Domains;


namespace GBReaderHanusH.Repository.Repository;

public interface IRepository
{
    public bool LoadBook();
    Library Library { get; set; }
    string Path { get; set; }
}