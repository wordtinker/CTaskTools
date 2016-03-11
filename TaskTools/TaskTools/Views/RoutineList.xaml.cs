using System.Windows;
using System.Windows.Controls;
using TaskTools.ViewModels;

namespace TaskTools.Views
{
    /// <summary>
    /// Interaction logic for RoutineList.xaml
    /// </summary>
    public partial class RoutineList : Window
    {
        public RoutineList()
        {
            InitializeComponent();
        }

        public void Routine_DoubleClick(object sender, RoutedEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                RoutineViewModel routineVM = (RoutineViewModel)row.DataContext;
                RoutineListViewModel listVM = (RoutineListViewModel)DataContext;
                listVM.EditRoutine(routineVM);
            }
        }
    }
}
