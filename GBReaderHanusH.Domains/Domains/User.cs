namespace GBReaderHanusH.Domains.Domains;

public class User
{
    public User(string firstname, string name)
    {
        Firstname = firstname;
        Name = name;
    }

    public string Firstname
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }

    public override string ToString() => Firstname + " " + Name;
    
}