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
            if (taskName.Text.Length > 2 && !string.IsNullOrEmpty(taskDate.Text) && taskDescription.Text.Length < 50 && !string.IsNullOrEmpty(taskType.Text))
            {
                string t = taskName.Text + "," + taskDate.Text + "," + taskDescription.Text+"\n";
                File.AppendAllText("tasks.txt", t);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                if (taskName.Text.Length < 3){ taskNameLabel.Content = "Příliš krátké";} else { taskNameLabel.Content = ""; }
                if (string.IsNullOrEmpty(taskDate.Text)) { taskDateLabel.Content = "Příliš krátké";} else { taskDateLabel.Content = ""; }
                if (taskDescription.Text.Length > 50){ taskDescriptionLabel.Content = "Příliš dlouhé";} else { taskDescriptionLabel.Content = ""; }
                if (taskType.Text.Length == 0) { taskTypeLabel.Content = "Vyberte prosím";} else { taskTypeLabel.Content = ""; }
            }
        }
    }
}
