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
            var vm = new UserViewModel(((App)App.Current).ApiService);
            vm.GetUsersCommand.Execute(0);
            DataContext = vm;
        }
    }
}