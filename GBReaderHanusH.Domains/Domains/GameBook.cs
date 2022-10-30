using System.Reflection.Metadata;

namespace GBReaderHanusH.Domains.Domains;

public class GameBook
{
    public GameBook(string title, string resume, string isbn, User user)
    {
        Title = title;
        Resume = resume;
        Isbn = isbn;
        User = user;
    }

    public string Title { get; set; }

    public string Isbn { get; set; }

    public string Resume { get; set; }

    public User User { get; set; }

    public override string ToString() => Isbn + "|" +Title+"|"+ User.ToString()+"|"+Resume;

    public string ToDetail() => "Auteur: " + User.ToString() + "\n\nISBN: " + Isbn + "\n\nTitre: " + Title + "\n\nRésumé: " + Resume;
}