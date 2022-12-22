using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.DTO;
using GBReaderHanusH.Repository.Repository;
using GBReaderHanusH.Repository.CustomException;
using Newtonsoft.Json;
using GBReaderHanusH.Domains.Domains;

namespace GBReaderHanusH.Infrastructure.Repository;

public class JsonRepository : IRepository
{

    public JsonRepository(ISessionMapper mapper, History history, string path)
    {
        Mapper = mapper;
        History = history;
        Path = path;
    }
    public ISessionMapper Mapper
    {
        get;
        set;
    }
    public History History
    {
        get;
        set;
    }
    public string Path
    {
        get;
        set;
    }

    public void LoadHistory()
    {
        DtoHistory dtoHistory;
        dtoHistory = ImportFile();
        History = Mapper.DtoHistoryToHistory(dtoHistory);
    }

    private DtoHistory ImportFile()
    {
        var jsonString = "";
        var pathComplete = Path + @"\ue36\190533-session.json";
        try
        {
            if (File.Exists(pathComplete))
            {
                using StreamReader reader = new(pathComplete);
                    jsonString = reader.ReadToEnd();
                    reader.Close();
            }
            else
            {
                throw new EmptyJsonFileException();
            }
            IDictionary<string, DtoSession>? listSession = JsonConvert.DeserializeObject<IDictionary<string, DtoSession>>(jsonString);
            return new(listSession);
        }
        catch (JsonReaderException)
        {
            throw new NotFormatJsonException();
        }
        
        
    }

    public void SaveHistory(History history)
    {
        var pathComplete = Path + @"\ue36\190533-session.json";
        DtoHistory dtoHistory = Mapper.HistoryToDtoHistory(history);
        string output = JsonConvert.SerializeObject(dtoHistory.DtoListSession);
        if (output == null) { throw new NotAbleSaveException(); }
        CheckDirectory();
            try
            {
                using FileStream fs = new(pathComplete, FileMode.Create);
                {
                    using TextWriter writer = new StreamWriter(fs);
                    {
                        writer.WriteLine(output);
                        writer.Close();
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                throw new NotAbleSaveException();
            }
        catch (IOException)
        {
            throw new NotAbleSaveException();
        }
        
    }

    private void CheckDirectory()
    {

        
            if (!Directory.Exists(Path + @"\ue36"))
            {
                Directory.CreateDirectory(Path + @"\ue36");
             
            }
            if (!File.Exists(Path + @"\ue36\190533-session.json"))
            {
            using FileStream fs = File.Create(Path + @"\ue36\190533-session.json");
        }
    }

}