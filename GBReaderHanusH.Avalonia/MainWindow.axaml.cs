using System;
using System.Collections.Generic;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Presenter.Events;
using Presenter.mvp;


namespace GBReaderHanusH.Avalonia
{

    public partial class MainWindow : Window,IMainView
    {
        private readonly AvaloniaList<string> _gameBookList = new AvaloniaList<string>();
        private  string _details="";
        public MainWindow()
        {
            
            InitializeComponent();
            LibraryList.Items = _gameBookList;
            ResumeText.Text = _details;
            Deux.IsVisible = false;
            Trois.IsVisible = false;

        }
      
        private void Resume_OnClick(object? sender, SelectionChangedEventArgs args)
        {
            object? selectedItem = LibraryList.SelectedItem;
            var element= selectedItem.ToString();
           GamebookSelected ?.Invoke(this,new GameBookEventArgs(element));
           
        }
        private void Research_OnClick(object? sender, RoutedEventArgs args)
        {
            var element= Research.Text;
            SearchGameBook ?.Invoke(this,new GameBookEventArgs(element));
            
        }

        public void PushMessage(string color, string message) {
            Message.Foreground = SolidColorBrush.Parse(color);
            Message.Text = message;
        }

        public void GetContentView(bool fileLoaded)
        {
            if (fileLoaded) { Content.IsVisible = true; } else { Content.IsVisible = false; }
        }
       

        public IEnumerable<string> Items
        {
            get =>_gameBookList;
            set
            {
                _gameBookList.Clear();
                _gameBookList.AddRange(value);
                LibraryList.Items = _gameBookList;
            }
        }
        public string GameBookDetail
        {
            get => _details;
            set
            {
                _details = value;
                ResumeText.Text = _details;
             }
        }


        public event GameBookEventHandler? GamebookSelected;
        public event GameBookEventHandler? SearchGameBook;

    }
}