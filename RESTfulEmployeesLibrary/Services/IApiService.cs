using RESTfulEmployeesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulEmployeesLibrary.Services
{
    public interface IApiService
    {
        Task<IList<User>> GetUsers(int? page, string searchName);
        Task<User> GetUser(int id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
