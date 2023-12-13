using RESTfulEmployees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulEmployees.Services
{
    public interface IApiService
    {
        Task<IList<User>?> GetUsers();
    }
}
