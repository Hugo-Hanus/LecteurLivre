using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Presenter.Events;
using System;

namespace GBReaderHanusH.Avalonia.Controls
{
    public partial class ChoiceView : UserControl
    {
        public ChoiceView() => InitializeComponent();
        public event EventHandler<ChoiceEventArgs> ChoiceMake;

        public string ChoiceName
        {
            get => ButtonChoice.Content.ToString()??"";

            set => ButtonChoice.Content = value;
        }

        public IBrush Color
        {
            get => ButtonChoice.Background ?? new SolidColorBrush(Colors.Red);
            set => ButtonChoice.Background = value;
        }
        public void ChoiceOnClick(object? sender, RoutedEventArgs args)
        {
            var content= ButtonChoice.Content.ToString()??"";
            string numberPage=content[..1];
            ChoiceMake?.Invoke(sender, new ChoiceEventArgs(numberPage));
            
        }
    }
}