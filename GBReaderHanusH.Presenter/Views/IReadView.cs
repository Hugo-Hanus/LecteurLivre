using Presenter.Events;

namespace Presenter.Views
{
    public interface IReadView
    {
        event EventHandler? GoToHome;
        event EventHandler? SaveSession;
        event EventHandler<ChoiceEventArgs>? ChoiceMake;

        string GameBookIsbn { get; set; }
        string GameBookTitle { get; set; }
        string PageText { get; set; }
        int PageNumber { get; set; }
        void AddChoice(int nbPage,string text,bool finish);
        void ClearChoice();
    }
}