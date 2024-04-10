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

namespace todo
{
    public partial class tvorba_úkolu : Window
    {
        public tvorba_úkolu()
        {
            InitializeComponent();
        }

        private void newTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskName.Text.Length > 2 && !string.IsNullOrEmpty(taskDate.Text) && taskDescription.Text.Length < 50 && !string.IsNullOrEmpty(taskType.Text) && !taskName.Text.Contains("*") && !taskDate.Text.Contains("*") && !taskDescription.Text.Contains("*") && !taskType.Text.Contains("*"))
            {
                string t = taskName.Text + "*" + taskDate.Text + "*" + taskDescription.Text + "*" + taskType.Text + "\n";
                File.AppendAllText("tasks.txt", t);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                taskNameLabel.Content = (taskName.Text.Length < 3 || taskName.Text.Contains("*")) ? (taskName.Text.Length < 3 ? "Příliš krátké" : "Nesmí obsahovat *") : "";

                taskDateLabel.Content = (string.IsNullOrEmpty(taskDate.Text) || taskDate.Text.Contains("*")) ? (string.IsNullOrEmpty(taskDate.Text) ? "Příliš krátké" : "Nesmí obsahovat *") : "";

                taskDescriptionLabel.Content = (taskDescription.Text.Length > 50 || taskDescription.Text.Contains("*")) ? (taskDescription.Text.Length > 50 ? "Příliš dlouhé" : "Nesmí obsahovat *") : "";

                taskTypeLabel.Content = (taskType.Text.Length == 0 || taskType.Text.Contains("*")) ? (taskType.Text.Length == 0 ? "Vyplňte prosím" : "Nesmí obsahovat *") : "";
            }
        }
    }
}
