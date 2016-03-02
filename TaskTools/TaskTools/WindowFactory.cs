using TaskTools.ViewModels;
using TaskTools.Views;

namespace TaskTools
{
    public interface IWindowFactory
    {
        void CreateEditor();
        void CreateEditor(TDTaskViewModel taskVM);
    }

    class WindowFactory : IWindowFactory
    {
        private MainWindow mainWindow;

        public WindowFactory(MainWindow mainWindow)
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
    }
}
