namespace GBReaderHanusH.Infrastructure.DTO;

public class DtoGameBook
{
    public DtoGameBook(string title, string isbn, string resume, DtoUser userDto)
    {
        Isbn = isbn;
        Title = title;
        Resume = resume;
        UserDto = userDto;
    }

    public string Isbn
    {
        get;
        set;
    }
    public string Title { get; set; }
    public string Resume { get; set; }
    public DtoUser UserDto { get; set; }
    
}