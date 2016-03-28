using Microsoft.Win32;
using System.Windows;
using TaskTools.ViewModels;
using TaskTools.Views;

namespace TaskTools
{
    class MainWindowService : IUIMainWindowService
    {
        private MainWindow mainWindow;

        public MainWindowService(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void CreateEditor()
        {
            TDTaskViewModel taskVM = new TDTaskViewModel();
            CreateEditor(taskVM);
        }

        public void CreateEditor(TDTaskViewModel taskVM)
        {
            TaskWindow window = new TaskWindow();
            window.DataContext = taskVM;
            window.Owner = mainWindow;
            window.Show();
        }

        public void CreateRoutineList()
        {
            RoutineList window = new RoutineList();
            window.Owner = mainWindow;
            window.Show();
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public string OpenFileDialog(string fileExtension)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = fileExtension;
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public string SaveFileDialog(string fileExtension)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = fileExtension;
            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public void Shutdown()
        {
            App.Current.Shutdown();
        }

        public void SetConfig(string key, string value)
        {
            Config.AddUpdateConfig(key, value);
        }

        public string GetConfig(string key)
        {
            return Config.ReadSetting(key);
        }
    }
}
