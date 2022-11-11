using System;
using System.Collections.Generic;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Org.BouncyCastle.Asn1.Crmf;
using Presenter.Events;
using Presenter.Views;

namespace GBReaderHanusH.Avalonia.Pages
{
    public partial class HomePage : UserControl,IHomeView
    {
        private readonly AvaloniaList<string> _gamebookList=new();
        private string? _details = "";
        public HomePage()
        {
            InitializeComponent();
            ResumeText.Text = _details;
            LibraryList.Items=_gamebookList;
            this.Initialized += OnInitialized;
        }
        public event EventHandler? GoToStats;
        public event EventHandler? AfterLoad;

        public event EventHandler<ReadingBookEventArgs>? ReadingBook;
        public event EventHandler<GameBookEventArgs>? GameBookSelected;
        public event EventHandler<GameBookEventArgs>? SearchGameBook;


        private void Resume_OnClick(object? sender, SelectionChangedEventArgs args)
        {
            object selectedItem = LibraryList.SelectedItem??new object();
            var element= selectedItem.ToString()??"";
                ResumeBlock.IsVisible = true;
                GameBookSelected?.Invoke(this, new GameBookEventArgs(element));
            
        }
        private void Research_OnClick(object? sender, RoutedEventArgs args)
        {
            var element= Research.Text;
            ResumeBlock.IsVisible = false;
            SearchGameBook ?.Invoke(this,new GameBookEventArgs(element));
            Research.Text="";
        }

        private void Read_OnClick(object? sender, RoutedEventArgs args)
        {
            object selectedItem = LibraryList.SelectedItem??new object();
            var selectString = selectedItem.ToString() ?? "";
            var element = selectString[..13];
            ReadingBook?.Invoke(this, new ReadingBookEventArgs(element));
        }

        private void Stats_OnClick(object? sender, RoutedEventArgs e) => GoToStats?.Invoke(this, EventArgs.Empty);

        public IEnumerable<string> Items { get => _gamebookList;
            set {
                _gamebookList.Clear();
                _gamebookList.AddRange(value);
                LibraryList.Items=_gamebookList;
            }
        }
        public IEnumerable<string> ResearchItems { get; set; }

        public string GameBookDetail
        {
            get => _details??"";
            set
            {
                _details = value;
                ResumeText.Text = _details;
            }
        }

        private void OnInitialized(object? sender ,EventArgs args )
        {
            
            AfterLoad?.Invoke(sender, args);
            if (LibraryList.ItemCount == 0)
            {
                ResumeBlock.IsVisible = false;
            }
            else
            {
                LibraryList.SelectedIndex = 0;
                ResumeBlock.IsVisible = true;
            }
        }

    }
}