namespace Presenter.Views
{
    public interface IStatisticView
    {
        event EventHandler? GoToHome;
        event EventHandler? LoadHistoryFile;
        IEnumerable<string> Items { get; set; }
        string NumberReading
        {
            get;    set;
        }
    }
}