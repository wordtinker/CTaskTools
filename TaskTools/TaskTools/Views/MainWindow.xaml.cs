using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TaskTools.ViewModels;

namespace TaskTools
{
    public class BrushColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

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
