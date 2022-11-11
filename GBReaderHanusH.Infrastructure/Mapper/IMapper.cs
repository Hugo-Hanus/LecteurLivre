using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;

namespace GBReaderHanusH.Infrastructure.Mapper;

public interface IMapper
{
    DtoHistory HistoryToDtoHistory(History history);
     History DtoHistoryToHistory(DtoHistory dtoHistory);
}