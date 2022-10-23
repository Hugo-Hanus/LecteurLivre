using GBReaderHanusH.Domains.Domains;

namespace GBReaderHanusH.Infrastructure.DTO;

public class DtoLibrary
{
    public DtoLibrary(SortedDictionary<string,DtoGameBook> library)
    {
        DtoLibrarie = library;
    }

    public SortedDictionary<string, DtoGameBook> DtoLibrarie
    {
        get;
        set;
    }

}