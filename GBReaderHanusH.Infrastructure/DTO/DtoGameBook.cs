using GBReaderHanusH.Domains.Domains;

namespace GBReaderHanusH.Infrastructure.DTO;

public class DtoGameBook
{
    public DtoGameBook(string title, string isbn, string resume, DtoUser userDto, IDictionary<int, DtoPage> pages)
    {
        Isbn = isbn;
        Title = title;
        Resume = resume;
        UserDto = userDto;
        Pages = pages;
    }

    public string Isbn
    {
        get;
        set;
    }
    public string Title { get; set; }
    public string Resume { get; set; }
    public IDictionary<int, DtoPage> Pages { get; set; }
    public DtoUser UserDto { get; set; }
    
}