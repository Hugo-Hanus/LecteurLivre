
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using GBReaderHanusH.Avalonia.Controls;
using Presenter.Events;
using Presenter.Views;
using System;


namespace GBReaderHanusH.Avalonia.Pages
{
    public partial class ReadPage : UserControl, IReadView
    {
        private string? _title, _isbn, _text, _number = "";
        public ReadPage()
        {
            InitializeComponent();
            Initialized += OnInitialized;
        }

        public event EventHandler? GoToHome;
        public event EventHandler? SaveSession;
        public event EventHandler<ChoiceEventArgs>? ChoiceMake;
        public void AddChoice(int nbPage, string text, bool finish)
        {
            ChoiceView choiceView = new();
            if (finish)
            {
                choiceView.ButtonChoice.Background = new SolidColorBrush(Color.FromRgb(254, 74, 73));
            }
            else
            {
                choiceView.ButtonChoice.Background = new SolidColorBrush(Color.FromRgb(242, 226, 186));
            }

            if (nbPage >= 0)
            {
                choiceView.ChoiceName = $"{nbPage}. {text}";
            }
            else
            {
                choiceView.ChoiceName = $"{text}";
            }
            choiceView.ChoiceMake += ChoiceMake;
            Choices.Children.Add(choiceView);
        }

        public void ClearChoice() => Choices.Children.Clear();

        private void Home_OnClick(object? sender, RoutedEventArgs e)
        {
            ClearChoice();
            GoToHome?.Invoke(this, EventArgs.Empty);
        }

        private void OnInitialized(object? sender, EventArgs args)
        {
            if (this.Parent is Window parentWindow)
            {
                parentWindow.Closed += OnWindowClosing;
            }
        }
        private void OnWindowClosing(object? sender, EventArgs e) => SaveSession?.Invoke(this, EventArgs.Empty);


        public string GameBookIsbn
        {
            get => _isbn??"";
            set
            {
                _isbn = value;
                IsbnText.Text = _isbn;
            }


        }
        public string GameBookTitle
        {
            get => _title??"";
            set
            {
                _title = value;
                TitleText.Text = _title;
            }
        }
        public int PageNumber
        {
            get => int.Parse(_number??"");
            set
            {
                _number = $"{value}";
                PageNumberText.Text = _number;
            }
        }
        public string PageText
        {
            get => _text??"";
            set
            {
                _text = value;
                TextPage.Text = value;
            }
        }
    }
}