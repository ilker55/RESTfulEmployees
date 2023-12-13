using RESTfulEmployeesLibrary.Models;
using RESTfulEmployeesLibrary.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RESTfulEmployeesLibrary.ViewModels
{
    public class UserViewModel
    {
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

        public ICommand GetUsersCommand { get; }

        public User SelectedUser { get; set; }

        public UserViewModel(IApiService apiService)
        {
            GetUsersCommand = new RelayCommand(async (page) =>
            {
                var users = await apiService.GetUsers((int?)page ?? 0);
                Users.Clear();
                if (users == null)
                    return;
                foreach (var user in users)
                    Users.Add(user);
            });

            GetUsersCommand.Execute(0);
        }

        private class RelayCommand : ICommand
        {
            private readonly Action<object> _executeAction;
            public RelayCommand(Action<object> executeAction)
            {
                _executeAction = executeAction;
            }
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => _executeAction(parameter);

            public event EventHandler CanExecuteChanged;
        }
    }
}
