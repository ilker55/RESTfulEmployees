using RESTfulEmployeesLibrary.ViewModels;
using System.Windows;

namespace RESTfulEmployees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new UserViewModel(((App)App.Current).ApiService);
        }
    }
}