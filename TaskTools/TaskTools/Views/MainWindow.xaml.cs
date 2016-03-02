using System.Windows;
using System.Windows.Controls;
using TaskTools.ViewModels;

namespace TaskTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowFactory factory = new WindowFactory(this);
            this.DataContext = new MainWindowViewModel(factory);
            InitializeComponent();
        }

        public void Task_DoubleClick(object sender, RoutedEventArgs e)
        {
            ListViewItem lv = sender as ListViewItem;
            if (lv != null)
            {
                TDTaskViewModel taskVM = (TDTaskViewModel)lv.DataContext;
                MainWindowViewModel mainVM = (MainWindowViewModel)DataContext;
                mainVM.EditTask(taskVM);
            }
        }
    }
}
