using Presenter.Events;

namespace Presenter.mvp
{
    public interface IMainView
    {
        IEnumerable<string> Items { get; set; }
        string GameBookDetail { get; set; }

        void PushMessage(string color, string message);
        void GetContentView(bool fileLoaded);

        event GameBookEventHandler GamebookSelected;
        event GameBookEventHandler SearchGameBook;
    }
}