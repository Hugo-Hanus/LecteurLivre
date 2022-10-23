using System.Collections;
using Avalonia.Controls;


namespace GBReaderHanusH.Avalonia
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Library.Items = "List";
            Message.Text = "PAS DE LIVRE";
        }
    }
}