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
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace todo
{
    public partial class úprava_úkolu : Window
    {
        public int index;
        public úprava_úkolu(string task, int index2)
        {
            InitializeComponent();
            string[] splt = task.Split(',');
            taskName.Text = splt[0];
            taskDate.Text = splt[1];
            taskDescription.Text = splt[2];
            index = index2;

        }

        private void editTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskName.Text.Length > 2 && !string.IsNullOrEmpty(taskDate.Text) && taskDescription.Text.Length < 50)
            {
                string t = taskName.Text + "," + taskDate.Text + "," + taskDescription.Text + "\n";
                File.AppendAllText("tasks.txt", t);
                List<string> lines = new List<string>(File.ReadAllLines("tasks.txt"));
                lines.RemoveAt(index);
                File.WriteAllLines("tasks.txt", lines);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                if (taskName.Text.Length < 3) { taskNameLabel.Content = "Příliš krátké"; } else { taskNameLabel.Content = ""; }
                if (string.IsNullOrEmpty(taskDate.Text)) { taskDateLabel.Content = "Příliš krátké"; } else { taskDateLabel.Content = ""; }
                if (taskDescription.Text.Length > 50) { taskDescriptionLabel.Content = "Příliš dlouhé"; } else { taskDescriptionLabel.Content = ""; }
            }
        }
    }
}
