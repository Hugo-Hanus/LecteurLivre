using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.DTO;
using GBReaderHanusH.Infrastructure.Repository;

namespace GBReaderHanusH.Infrastructure.Mapper;

public class MapperOne:IMapper
{
    public DtoLibrary LibraryToDtoLibrary(Library library)
    {
        SortedDictionary<string, DtoGameBook> dtoLibrary = new SortedDictionary<string, DtoGameBook>();
        DtoLibrary dto = new DtoLibrary(dtoLibrary);
        foreach (var list in library.GetLibrary())
        {
            dto.DtoLibrarie.Add(list.Key,GameBookToDtoGameBook(list.Value));
        }

        return dto;
    }

    public Library DtoLibraryToLibrary(DtoLibrary dto)
    {
        IDictionary<string, GameBook> newLibrary = new SortedDictionary<string, GameBook>();
        if (dto == null) throw new NotFormatJsonException();
        foreach (var list in dto.DtoLibrarie)
        {
            newLibrary.Add(list.Key,DtoGameBookToGameBook(list.Value));
        }

        Library lib = new Library();
        lib.SetLibrary(newLibrary);
        return lib;
    }

    private static DtoGameBook GameBookToDtoGameBook(GameBook gb)
    {
        DtoGameBook dto = new DtoGameBook(gb.Title, gb.Isbn, gb.Resume, new DtoUser(gb.User.Firstname, gb.User.Name));
        return dto;
    }

    private static GameBook DtoGameBookToGameBook(DtoGameBook dto)
    {
        return new GameBook(dto.Title,dto.Resume,dto.Isbn,new User(dto.UserDto.Firstname,dto.UserDto.Name));
    }
}