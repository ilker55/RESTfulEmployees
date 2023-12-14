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

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public UserViewModel(IApiService apiService)
        {
            _apiService = apiService;
            GetUsersCommand = new RelayCommand(async (page) =>
            {
                var users = await GetUsers((int?)page ?? 0, null);

                // Clear and fill list with new users
                Users.Clear();
                foreach (var user in users)
                    Users.Add(user);
            });
        }

        public async Task<IList<User>> GetUsers(int? page, string searchName)
        {
            // Get all users for the given page and search name
            var users = await _apiService.GetUsers(page, searchName);

            // Return users or empty list on null
            return users ?? new List<User>();
        }

        public async Task<User> GetUser(int id)
        {
            // Get user for given ID
            return await _apiService.GetUser(id);
        }

        public async Task<User> CreateUser(User user)
        {
            // Create user with given data
            return await _apiService.CreateUser(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            // Update user with given data
            return await _apiService.UpdateUser(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            // Delete user for given ID
            return await _apiService.DeleteUser(id);
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
