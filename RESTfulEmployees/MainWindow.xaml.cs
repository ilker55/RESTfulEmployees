using RESTfulEmployees.Services;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            GetUsers();
        }

        async void GetUsers()
        {
            var users = await ((App)App.Current).ApiService.GetUsers();
            if (users == null)
            {
                MessageBox.Show("Failed to get users", "Error", MessageBoxButton.OK);
                return;
            }

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
            }
            MessageBox.Show($"Retreived {users.Count} users", "Response", MessageBoxButton.OK);
        }
    }
}