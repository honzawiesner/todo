using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Schema;

namespace todo
{
    public partial class MainWindow : Window
    {
        public List<Button> editButtons = new List<Button>();
        public List<Button> deleteButtons = new List<Button>();
        public bool delete;

        public MainWindow()
        {
            InitializeComponent();

            writeOut();
        }


        private void newTask_Click(object sender, RoutedEventArgs e)
        {
            tvorba_úkolu tvorba_Úkolu = new tvorba_úkolu();
            tvorba_Úkolu.ShowDialog();

            writeOut();
        }

        public void writeOut()
        {
            stackP.Children.Clear();
            if (File.Exists("tasks.txt")){}
            else{File.Create("tasks.txt");}

            string[] s = File.ReadAllLines("tasks.txt");

            editButtons.Clear();
            deleteButtons.Clear();

            foreach (string s2 in s)
            {
                string[] splt = s2.Split('*');
                makeTask(splt);
            }
        }

        public void makeTask(string[] splt)
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(0, 0, 0, 1);
            border.BorderBrush = Brushes.DarkGray;

            Grid mainGrid = new Grid();
            mainGrid.Height = 80;

            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(90, GridUnitType.Star);
            ColumnDefinition column3 = new ColumnDefinition();
            column3.Width = new GridLength(12, GridUnitType.Star);
            ColumnDefinition column4 = new ColumnDefinition();
            column4.Width = new GridLength(1, GridUnitType.Star);

            mainGrid.ColumnDefinitions.Add(column1);
            mainGrid.ColumnDefinitions.Add(column2);
            mainGrid.ColumnDefinitions.Add(column3);
            mainGrid.ColumnDefinitions.Add(column4);

            TextBlock textBlock = new TextBlock();
            Grid.SetColumn(textBlock, 0);

            switch (splt[3])
            {
                case "Škola":
                    textBlock.Background = Brushes.IndianRed;
                    break;
                case "Osobní":
                    textBlock.Background = Brushes.LimeGreen;
                    break;
                case "Práce":
                    textBlock.Background = Brushes.RoyalBlue;
                    break;
                case "Jiné":
                    textBlock.Background = Brushes.Yellow;
                    break;
                default:
                    break;
            }

            mainGrid.Children.Add(textBlock);

            Grid nestedGrid = new Grid();
            Grid.SetColumn(nestedGrid, 1);

            ColumnDefinition nestedColumn1 = new ColumnDefinition();
            nestedColumn1.Width = new GridLength(2, GridUnitType.Star);
            ColumnDefinition nestedColumn2 = new ColumnDefinition();
            nestedColumn2.Width = new GridLength(3, GridUnitType.Star);
            nestedGrid.ColumnDefinitions.Add(nestedColumn1);
            nestedGrid.ColumnDefinitions.Add(nestedColumn2);

            Grid nameDateGrid = new Grid();
            Grid.SetColumn(nameDateGrid, 0);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            nameDateGrid.RowDefinitions.Add(row1);
            nameDateGrid.RowDefinitions.Add(row2);

            TextBlock nameTextBlock = new TextBlock();
            nameTextBlock.Text = splt[0];
            nameTextBlock.FontWeight = FontWeights.Bold;
            nameTextBlock.Margin = new Thickness(10);
            TextBlock dateTextBlock = new TextBlock();
            dateTextBlock.Text = splt[1];
            DateTime date = Convert.ToDateTime(splt[1]);
            if (date < DateTime.Now)
            {
                dateTextBlock.Foreground = Brushes.Red;
                dateTextBlock.FontWeight = FontWeights.Bold;
            }
            dateTextBlock.Margin = new Thickness(10);
            Grid.SetRow(dateTextBlock, 1);

            nameTextBlock.VerticalAlignment = VerticalAlignment.Center;
            dateTextBlock.VerticalAlignment = VerticalAlignment.Center;

            nameDateGrid.Children.Add(nameTextBlock);
            nameDateGrid.Children.Add(dateTextBlock);

            Grid descriptionGrid = new Grid();
            Grid.SetColumn(descriptionGrid, 1);

            RowDefinition row3 = new RowDefinition();
            row3.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition row4 = new RowDefinition();
            row4.Height = new GridLength(7, GridUnitType.Star);
            RowDefinition row5 = new RowDefinition();
            row5.Height = new GridLength(1, GridUnitType.Star);
            descriptionGrid.RowDefinitions.Add(row3);
            descriptionGrid.RowDefinitions.Add(row4);
            descriptionGrid.RowDefinitions.Add(row5);

            TextBlock descriptionTextBlock = new TextBlock();
            descriptionTextBlock.Text = splt[2];
            descriptionTextBlock.TextWrapping = TextWrapping.Wrap;
            descriptionTextBlock.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(descriptionTextBlock, 1);

            descriptionGrid.Children.Add(descriptionTextBlock);

            nestedGrid.Children.Add(nameDateGrid);
            nestedGrid.Children.Add(descriptionGrid);

            Grid buttonGrid = new Grid();
            Grid.SetColumn(buttonGrid, 2);

            RowDefinition row6 = new RowDefinition();
            RowDefinition row7 = new RowDefinition();
            buttonGrid.RowDefinitions.Add(row6);
            buttonGrid.RowDefinitions.Add(row7);

            Style buttonStyle = (Style)FindResource("ButtonStyle");

            Button editButton = new Button();
            editButton.Content = "✎";
            editButton.FontSize = 15;
            editButton.Style = buttonStyle;
            editButton.Click += EditButton_Click;
            editButtons.Add(editButton);

            Button deleteButton = new Button();
            deleteButton.Content = "🗑";
            deleteButton.FontSize = 10;
            deleteButton.Style = buttonStyle;
            deleteButton.Click += DeleteButton_Click;
            deleteButtons.Add(deleteButton);

            Grid.SetRow(editButton, 0);
            Grid.SetRow(deleteButton, 1);

            buttonGrid.Children.Add(editButton);
            buttonGrid.Children.Add(deleteButton);

            mainGrid.Children.Add(nestedGrid);
            mainGrid.Children.Add(buttonGrid);

            border.Child = mainGrid;

            stackP.Children.Add(border);
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            int index = editButtons.IndexOf(editButton);

            string[] s = File.ReadAllLines("tasks.txt");

            úprava_úkolu úprava_Úkolu = new úprava_úkolu(s[index], index);
            úprava_Úkolu.ShowDialog();

            writeOut();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            int index = deleteButtons.IndexOf(deleteButton);

            List<string> lines = new List<string>(File.ReadAllLines("tasks.txt"));
            lines.RemoveAt(index);
            File.WriteAllLines("tasks.txt", lines);

            writeOut();
        }
    }
}
