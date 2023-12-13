using RESTfulEmployees.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RESTfulEmployees.ViewModels
{
    public class UsersViewModel
    {
        public ObservableCollection<User>? Users { get; } = new ObservableCollection<User>();

        public ICommand? GetUsersCommand { get; }

        public User? SelectedUser { get; set; }

        public UsersViewModel()
        {
            GetUsersCommand = new RelayCommand(async (page) =>
            {
                var users = await ((App)App.Current).ApiService.GetUsers((int?)page ?? 0);
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
            private readonly Action<object?> _executeAction;
            public RelayCommand(Action<object?> executeAction)
            {
                _executeAction = executeAction;
            }
            public bool CanExecute(object? parameter) => true;
            public void Execute(object? parameter) => _executeAction(parameter);

            public event EventHandler? CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
