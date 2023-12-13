using RESTfulEmployeesLibrary.Models;
using RESTfulEmployeesLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RESTfulEmployeesLibrary.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();

        public ICommand GetUsersCommand { get; }

        public User SelectedUser { get; set; }

        public UserViewModel(IApiService apiService)
        {
            _apiService = apiService;
            GetUsersCommand = new RelayCommand(async (page) =>
            {
                var users = await GetUsers(page);

                // Clear and fill list with new users
                Users.Clear();
                foreach (var user in users)
                    Users.Add(user);
            });
        }

        public async Task<IList<User>> GetUsers(object page)
        {
            // Get all users for the given page
            var users = await _apiService.GetUsers((int?)page ?? 0);

            // Return users or empty list on null
            return users ?? new List<User>();
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
