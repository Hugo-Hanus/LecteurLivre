namespace GBReaderHanusH.Infrastructure.DTO;

public class DtoUser
{
    public DtoUser(string firstname, string name)
    {
        Firstname = firstname;
        Name = name;
    }
    public string Firstname { get; set; }
    public string Name { get; set; }
}