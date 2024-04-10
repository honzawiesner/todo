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
            string[] splt = task.Split('*');
            taskName.Text = splt[0];
            taskDate.Text = splt[1];
            taskDescription.Text = splt[2];
            taskType.Text = splt[3];
            index = index2;
        }

        private void editTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskName.Text.Length > 2 && !string.IsNullOrEmpty(taskDate.Text) && taskDescription.Text.Length < 201 && !taskName.Text.Contains("*") && !taskDate.Text.Contains("*") && !taskDescription.Text.Contains("*") && !taskType.Text.Contains("*"))
            {
                string t = taskName.Text + "*" + taskDate.Text + "*" + taskDescription.Text + "*" + taskType.Text + "\n";
                File.AppendAllText("tasks.txt", t);
                List<string> lines = new List<string>(File.ReadAllLines("tasks.txt"));
                lines.RemoveAt(index);
                File.WriteAllLines("tasks.txt", lines);

                Close();
            }
            else
            {
                taskNameLabel.Content = (taskName.Text.Length < 3 || taskName.Text.Contains("*")) ? (taskName.Text.Length < 3 ? "Příliš krátké" : "Nesmí obsahovat *") : "";

                taskDateLabel.Content = (string.IsNullOrEmpty(taskDate.Text) || taskDate.Text.Contains("*")) ? (string.IsNullOrEmpty(taskDate.Text) ? "Příliš krátké" : "Nesmí obsahovat *") : "";

                taskDescriptionLabel.Content = (taskDescription.Text.Length > 200 || taskDescription.Text.Contains("*")) ? (taskDescription.Text.Length > 50 ? "Příliš dlouhé" : "Nesmí obsahovat *") : "";

                taskTypeLabel.Content = (taskType.Text.Length == 0 || taskType.Text.Contains("*")) ? (taskType.Text.Length == 0 ? "Vyplňte prosím" : "Nesmí obsahovat *") : "";
            }
        }
    }
}
