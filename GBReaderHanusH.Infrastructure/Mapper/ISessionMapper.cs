using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;

namespace GBReaderHanusH.Infrastructure.Mapper;

public interface ISessionMapper
{
    DtoHistory HistoryToDtoHistory(History history);
     History DtoHistoryToHistory(DtoHistory dtoHistory);
}