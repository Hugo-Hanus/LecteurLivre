using System.Reflection.Metadata;

namespace GBReaderHanusH.Domains.Domains;

public class GameBook
{
    public GameBook(string title, string resume, string isbn, User user,IDictionary<int,Page> pages)
    {
        Title = title;
        Resume = resume;
        Isbn = isbn;
        User = user;
        Pages = pages;
    }

    public string Title { get; set; }

    public string Isbn { get; set; }

    public string Resume { get; set; }

    public User User { get; set; }
    public IDictionary<int,Page> Pages { get; set; }

    public int GetFirstPage() => Pages.First().Key;

    public override string ToString() => Isbn + "|" +Title+"|"+ User.ToString()+"|"+Resume;

    public string ToDetail() => "Auteur: " + User.ToString() + "\n\nISBN: " + Isbn + "\n\nTitre: " + Title + "\n\nRésumé: " + Resume;
}