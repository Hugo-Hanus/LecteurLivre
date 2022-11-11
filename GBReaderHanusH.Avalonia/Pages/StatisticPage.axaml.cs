using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

using Presenter.Views;
using System;
using System.Collections.Generic;

namespace GBReaderHanusH.Avalonia.Pages
{
    public partial class StatisticPage : UserControl,IStatisticView
    {
        private readonly AvaloniaList<string> _gamebookList = new();
        private string _numberReading = "";
        public StatisticPage()
        {
            InitializeComponent();
            this.ResourcesChanged += LoadFile;     
        }

        public IEnumerable<string> Items { get => _gamebookList; 
            set {
                _gamebookList.Clear();
                _gamebookList.AddRange(value);
                StatisticList.Items = _gamebookList;
            } }

        public string NumberReading
        {
            get => _numberReading;
            set{
                _numberReading = value;
                NumberSession.Text = _numberReading;
            }
        }

        public event EventHandler? GoToHome;
        public event EventHandler? LoadHistoryFile;

        private void LoadFile(object? sender, ResourcesChangedEventArgs args) => LoadHistoryFile?.Invoke(this, EventArgs.Empty);
        private void Home_OnClick(object? sender, RoutedEventArgs e) => GoToHome?.Invoke(this, EventArgs.Empty);
    }
}