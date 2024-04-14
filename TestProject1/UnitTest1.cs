using todo;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTaskCreation()
        {
            // Arrange
            var taskName = "�kol 1";
            var taskDate = DateTime.Now;
            var taskDescription = "Popis �kolu 1";
            var taskType = "�kola";

            // Act
            var task = new todo.Task(taskName, taskDate, taskDescription, taskType);

            // Assert
            Assert.AreEqual(taskName, task.Name);
            Assert.AreEqual(taskDate, task.Date);
            Assert.AreEqual(taskDescription, task.Description);
            Assert.AreEqual(taskType, task.Type);
        }

        [TestMethod]
        public void TestMainWindowWriteOut()
        {
            // Arrange
            var mainWindow = new todo.MainWindow();
            var tasks = new List<todo.Task>
            {
                new todo.Task("�kol 1", DateTime.Now, "Popis �kolu 1", "�kola"),
                new todo.Task("�kol 2", DateTime.Now.AddDays(1), "Popis �kolu 2", "Pr�ce"),
                new todo.Task("�kol 3", DateTime.Now.AddDays(-1), "Popis �kolu 3", "Osobn�")
            };

            // Act
            foreach (var task in tasks)
            {
                string taskString = $"{task.Name}*{task.Date.ToShortDateString()}*{task.Description}*{task.Type}\n";
                System.IO.File.AppendAllText("tasks.txt", taskString);
            }

            mainWindow.writeOut();

            // Assert
            Assert.AreEqual(tasks.Count, mainWindow.stackP.Children.Count);
        }

        [TestMethod]
        public void TestEditButtonFunctionality()
        {
            // Arrange
            var mainWindow = new todo.MainWindow();
            var tasks = new List<todo.Task>
            {
                new todo.Task("�kol 1", DateTime.Now, "Popis �kolu 1", "�kola"),
                new todo.Task("�kol 2", DateTime.Now.AddDays(1), "Popis �kolu 2", "Pr�ce"),
                new todo.Task("�kol 3", DateTime.Now.AddDays(-1), "Popis �kolu 3", "Osobn�")
            };

            foreach (var task in tasks)
            {
                string taskString = $"{task.Name}*{task.Date.ToShortDateString()}*{task.Description}*{task.Type}\n";
                System.IO.File.AppendAllText("tasks.txt", taskString);
            }

            mainWindow.writeOut();

            // Act
            var editButton = mainWindow.editButtons[0];
            editButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));

            // Assert
            // Here you can add assertions to check if the edit dialog is opened or not
        }

        [TestMethod]
        public void TestDeleteButtonFunctionality()
        {
            // Arrange
            var mainWindow = new todo.MainWindow();
            var tasks = new List<todo.Task>
            {
                new todo.Task("�kol 1", DateTime.Now, "Popis �kolu 1", "�kola"),
                new todo.Task("�kol 2", DateTime.Now.AddDays(1), "Popis �kolu 2", "Pr�ce"),
                new todo.Task("�kol 3", DateTime.Now.AddDays(-1), "Popis �kolu 3", "Osobn�")
            };

            foreach (var task in tasks)
            {
                string taskString = $"{task.Name}*{task.Date.ToShortDateString()}*{task.Description}*{task.Type}\n";
                System.IO.File.AppendAllText("tasks.txt", taskString);
            }

            mainWindow.writeOut();

            // Act
            var deleteButton = mainWindow.deleteButtons[0];
            deleteButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));
        }
    }
}