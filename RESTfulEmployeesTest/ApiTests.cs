using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RESTfulEmployeesLibrary.Models;
using RESTfulEmployeesLibrary.Services;
using RESTfulEmployeesLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RESTfulEmployeesTest
{
    [TestClass]
    public class ApiTests
    {
        private Mock<IApiService> _mockApiService;

        [TestInitialize]
        public void Init()
        {
            _mockApiService = new Mock<IApiService>();
            _mockApiService.Setup(x => x.GetUsers(0)).ReturnsAsync(new List<User>
            {
                new User { Id = 1, Name = "Mock user 1" },
                new User { Id = 2, Name = "Mock user 2" },
                new User { Id = 3, Name = "Mock user 3" },
                new User { Id = 4, Name = "Mock user 4" }
            });
        }

        [TestMethod]
        public async Task TestGetUsers()
        {
            var userVM = new UserViewModel(_mockApiService.Object);
            var users = await userVM.GetUsers(0);

            Assert.AreEqual(4, users.Count);

            Assert.AreEqual("Mock user 1", users.FirstOrDefault()?.Name);
            Assert.AreEqual("Mock user 4", users.LastOrDefault()?.Name);
        }
    }
}
