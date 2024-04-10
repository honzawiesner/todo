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
            tvorba_Úkolu.Show();
            Close();
        }

        public void writeOut()
        {
            stackP.Children.Clear();
            if (File.Exists("tasks.txt")){}
            else{File.Create("tasks.txt");}

            string[] s = File.ReadAllLines("tasks.txt");
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
            mainGrid.Height = 70;

            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(8, GridUnitType.Star);
            ColumnDefinition column3 = new ColumnDefinition();
            column3.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition column4 = new ColumnDefinition();
            column4.Width = new GridLength(1, GridUnitType.Star);
            mainGrid.ColumnDefinitions.Add(column1);
            mainGrid.ColumnDefinitions.Add(column2);
            mainGrid.ColumnDefinitions.Add(column3);
            mainGrid.ColumnDefinitions.Add(column4);

            Grid nestedGrid = new Grid();
            Grid.SetColumn(nestedGrid, 1);

            ColumnDefinition nestedColumn1 = new ColumnDefinition();
            nestedColumn1.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition nestedColumn2 = new ColumnDefinition();
            nestedColumn2.Width = new GridLength(1, GridUnitType.Star);
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

            TextBlock descriptionTextBlock = new TextBlock();
            descriptionTextBlock.Text = splt[2];
            Grid.SetColumn(descriptionTextBlock, 1);

            nestedGrid.Children.Add(nameDateGrid);
            nestedGrid.Children.Add(descriptionTextBlock);

            Style buttonStyle = (Style)FindResource("ButtonStyle");

            Button editButton = new Button();
            editButton.Content = "✎";
            editButton.FontSize = 20;
            editButton.Style = buttonStyle;
            editButton.Click += EditButton_Click;
            editButtons.Add(editButton);
            editButton.Resources.Add(typeof(Border), new Style { TargetType = typeof(Border), Setters = { new Setter { Property = Border.CornerRadiusProperty, Value = new CornerRadius(50) } } });

            Button deleteButton = new Button();
            deleteButton.Content = "🗑";
            deleteButton.FontSize = 14;
            deleteButton.Style = buttonStyle;
            deleteButton.Click += DeleteButton_Click;
            deleteButtons.Add(deleteButton);
            deleteButton.Resources.Add(typeof(Border), new Style { TargetType = typeof(Border), Setters = { new Setter { Property = Border.CornerRadiusProperty, Value = new CornerRadius(50) } } });

            Grid.SetColumn(editButton, 2);
            Grid.SetColumn(deleteButton, 3);

            mainGrid.Children.Add(nestedGrid);
            mainGrid.Children.Add(editButton);
            mainGrid.Children.Add(deleteButton);

            border.Child = mainGrid;

            stackP.Children.Add(border);
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            int index = editButtons.IndexOf(editButton);

            string[] s = File.ReadAllLines("tasks.txt");

            úprava_úkolu úprava_Úkolu = new úprava_úkolu(s[index], index);
            úprava_Úkolu.Show();
            Close();
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
