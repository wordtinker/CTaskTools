using TaskTools.ViewModels;

namespace TaskTools.Views
{
    class RoutineListService : IUIRoutineListService
    {
        private RoutineList routineListWindow;

        public RoutineListService(RoutineList window)
        {
            this.routineListWindow = window;
        }

        public void CreateEditor()
        {
            RoutineViewModel routine = new RoutineViewModel();
            CreateEditor(routine);
        }

        public void CreateEditor(RoutineViewModel routine)
        {
            RoutineWindow window = new RoutineWindow();
            window.DataContext = new RoutineViewModel(routine);
            window.Owner = routineListWindow;
            window.Show();
        }
    }
}
