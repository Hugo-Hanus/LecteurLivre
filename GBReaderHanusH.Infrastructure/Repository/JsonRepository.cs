
using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.DTO;
using GBReaderHanusH.Repository.Repository;
using Newtonsoft.Json;


namespace GBReaderHanusH.Infrastructure.Repository;

public class JsonRepository : IRepository
{
    public JsonRepository(IMapper mapper, Library library, string path)
    {
        Mapper = mapper;
        Library = library;
        Path = path;
    }
    private IMapper Mapper
    {
        get;
        set;
    }
    public Library Library
    {
        get;
        set;
    }
    public string Path
    {
        get;
        set;
    }
    public bool LoadBook()
    {
        if (File.Exists(Path))
        {
            DtoLibrary dtoLibrary;
            try
            { dtoLibrary = ImportFile();
            }
            catch (EmptyJsonFileException)
            {
                return false;
            }
            try
            {
                Library = Mapper.DtoLibraryToLibrary(dtoLibrary);
            }
            catch (NotFormatJsonException)
            {
                return true;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private DtoLibrary? ImportFile()
    {
        try
        {
            using StreamReader reader = new StreamReader(Path);
            var jsonString = reader.ReadToEnd();
            DtoLibrary? dtoLibrary = JsonConvert.DeserializeObject<DtoLibrary>(jsonString);
            return dtoLibrary;
        }
        catch (JsonSerializationException)
        {
            throw new EmptyJsonFileException();
        }
    }
}