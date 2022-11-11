using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;
using GBReaderHanusH.Repository.CustomException;

namespace GBReaderHanusH.Infrastructure.Mapper;

public class MapperOne : IMapper
{
    public History DtoHistoryToHistory(DtoHistory dtoHistory)
    {
        IDictionary<string, Session> newHistory = new Dictionary<string, Session>();
        if (dtoHistory == null) { throw new NotFormatJsonException(); }
        if(dtoHistory.DtoListSession == null) { throw new EmptyJsonFileException(); }
        foreach (var session in dtoHistory.DtoListSession)
        {
            newHistory.Add(session.Key, DtoSessionToSession(session.Value));
        }
        History history = new()
        {
            SessionList = newHistory
        };
        return history;
    }

    private Session DtoSessionToSession(DtoSession dto) => new(dto.Begin, dto.LastUpdate,dto.Title, dto.Page);

    public DtoHistory HistoryToDtoHistory(History history)
    {
        Dictionary<string, DtoSession> dtoHistory=new Dictionary<string, DtoSession>();
        
        foreach(var session in history.SessionList)
        {
            dtoHistory.Add(session.Key, SessionToDtoSession(session.Value));
        }
        return new(dtoHistory);
    }

    private DtoSession SessionToDtoSession(Session session) => new DtoSession(session.Begin,session.LastUpdate,session.Title,session.Page);
}