using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using GBReaderHanusH.Domains.Domains;
using GBReaderHanusH.Infrastructure.Mapper;
using GBReaderHanusH.Infrastructure.Repository;
using GBReaderHanusH.Repository.Repository;


namespace GBReaderHanusH.Avalonia
{

    public partial class MainWindow : Window
    {
        private AvaloniaList<string> myList=new AvaloniaList<string>();
        private Library lib;

        public MainWindow()
        {
            InitializeComponent();
            IMapper mapper = new MapperOne();
            lib = new Library();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+@"\ue36\190533.json";
            IRepository jsonRepository = new JsonRepository(mapper, lib, path);
            if (jsonRepository.LoadBook())
            {
                lib=jsonRepository.Library;
                if (lib.SizeOfLibrary() > 0)
                {
                    myList= LibraryToList();
                    SetMessage("#007F00","Fichier chargé avec Succès");
                    LibraryList.Items = myList;
                    ResumeBlock.IsVisible = true;
                    ResumeText.Text = lib.GetLibrary().First().Value.Resume;
                }
                else
                {
                    ResearchBlock.IsVisible = false;
                    LibraryList.IsVisible = false;
                    SetMessage("#FFA500","Pas de livre dans le fichier");
                }
            }
            else
            {
                ResearchBlock.IsVisible = false;
                LibraryList.IsVisible = false;
                
                SetMessage("#ff0000", "Erreur lors du chargment du fichier");
            }
        }

        private AvaloniaList<string> LibraryToList()
        {
            foreach (var gameBook in lib.GetLibrary())
            {
                myList.Add(gameBook.Value.ToString());
            }

            return myList;
        }

        private void Resume_OnClick(object? sender, SelectionChangedEventArgs args)
        {
            ResumeBlock.IsVisible = true;
            var item = LibraryList.SelectedItem;
            var itemS = item.ToString();
            var isbn =itemS[..13];
            var a=lib.GetLibrary();
            ResumeText.Text = a[isbn].Resume;



        }
        private void Research_OnClick(object? sender, RoutedEventArgs args)
        {
            myList.Clear();
            string search = Research.Text;
            if (!search.Equals(""))
            {
                ResearchListUpdate(search);
            }
            else
            {
                myList = LibraryToList();
            }

            if (myList.Count == 0)
            {
                SetMessage("#FFA500", "Aucun résultat pour : " + search);
            }
            else
            {
                SetMessage("#000000", "Résultat pour : " + search);
            }
            LibraryList.Items= myList;
           

        }

        private void ResearchListUpdate(string search)
        {
            foreach (var keyValue in lib.GetLibrary())
            {
                if (keyValue.Key.Equals(search))
                {
                    myList.Add(keyValue.Value.ToString());
                }

                if (keyValue.Value.Title.Contains(search))
                {
                    myList.Add(keyValue.Value.ToString());
                }
            }
        }

        private void SetMessage(string color, string content)
        {
            Message.Foreground = SolidColorBrush.Parse(color);
            Message.Text = content;
        }
    }
}