using System.Windows;

namespace TaskTools.Views
{
    /// <summary>
    /// Interaction logic for RoutineWindow.xaml
    /// </summary>
    public partial class RoutineWindow : Window
    {
        public RoutineWindow()
        {
            InitializeComponent();
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
