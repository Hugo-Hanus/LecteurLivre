using Presenter.Events;

namespace Presenter.Views
{
    public interface IHomeView
    {
        event EventHandler<GameBookEventArgs>? GameBookSelected;
        event EventHandler<GameBookEventArgs>? SearchGameBook;
        event EventHandler<ReadingBookEventArgs>? ReadingBook;
        event EventHandler? GoToStats;
        event EventHandler? AfterLoad;
        string GameBookDetail { get; set; }

        IEnumerable<string> Items { get; set; }
        IEnumerable<string> ResearchItems { get; set; }
    }
}