using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;

namespace GBReaderHanusH.Infrastructure.Mapper;

public interface IMapper
{
     DtoLibrary LibraryToDtoLibrary(Library library);
     Library DtoLibraryToLibrary(DtoLibrary library);
}