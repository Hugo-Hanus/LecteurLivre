
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
    public IMapper Mapper
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
    
    
    //private readonly string? PATH = System.Environment.GetEnvironmentVariable("USERPROFILE")+"/ue36/190533.json";
    public bool LoadBook()
    {
        
        if (File.Exists(Path))
        {
            var dtoLibrary = ImportFile();
            if (dtoLibrary != null)
            {
                Library = Mapper.DtoLibraryToLibrary(dtoLibrary);
                return true;
            }
            else
            {
                return false;
            }
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
            using StreamReader rider = new StreamReader(Path);
            var jsonString = rider.ReadToEnd();
            DtoLibrary? dtoLibrary = JsonConvert.DeserializeObject<DtoLibrary>(jsonString);
            return dtoLibrary;
        }
        catch (JsonSerializationException e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}