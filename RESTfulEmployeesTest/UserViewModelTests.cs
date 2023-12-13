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
    public class UserViewModelTests
    {
        private Mock<IApiService> _mockApiService;

        [TestInitialize]
        public void Init()
        {
            _mockApiService = new Mock<IApiService>();
        }

        [TestMethod]
        public async Task TestGetUsers()
        {
            _mockApiService.Setup(x => x.GetUsers(0, null)).ReturnsAsync(new List<User>
            {
                new User { Id = 1, Name = "Mock user 1" },
                new User { Id = 2, Name = "Mock user 2" },
                new User { Id = 3, Name = "Mock user 3" },
                new User { Id = 4, Name = "Mock user 4" }
            });
            var userViewModel = new UserViewModel(_mockApiService.Object);
            var users = await userViewModel.GetUsers(0, null);

            Assert.AreEqual(4, users.Count);
            Assert.AreEqual("Mock user 1", users.FirstOrDefault()?.Name);
            Assert.AreEqual("Mock user 4", users.LastOrDefault()?.Name);
        }

        [TestMethod]
        public async Task TestSearchUsers()
        {
            _mockApiService.Setup(x => x.GetUsers(null, "Mock")).ReturnsAsync(new List<User>
            {
                new User { Id = 1, Name = "Mock user 1" },
                new User { Id = 2, Name = "Mock user 2" }
            });
            var userViewModel = new UserViewModel(_mockApiService.Object);
            var users = await userViewModel.GetUsers(null, "Mock");

            Assert.AreEqual(2, users.Count);
            Assert.AreEqual("Mock user 1", users.FirstOrDefault()?.Name);
            Assert.AreEqual("Mock user 2", users.LastOrDefault()?.Name);
        }

        [TestMethod]
        public async Task TestGetUser()
        {
            _mockApiService.Setup(x => x.GetUser(1)).ReturnsAsync(new User { Id = 1, Name = "Mock user 1" });
            var userViewModel = new UserViewModel(_mockApiService.Object);
            var user = await userViewModel.GetUser(1);

            Assert.IsNotNull(user);
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Mock user 1", user.Name);
        }

        [TestMethod]
        public async Task TestCreateUser()
        {
            var inputUser = new User
            {
                Name = "Mock user 1",
                Email = $"mock-{DateTime.Now.Ticks}@mock.test",
                Gender = "male",
                Status = "inactive"
            };
            var expectUser = inputUser.Clone();
            expectUser.Id = 1;

            _mockApiService.Setup(x => x.CreateUser(inputUser)).ReturnsAsync(expectUser);
            var userViewModel = new UserViewModel(_mockApiService.Object);
            var user = await userViewModel.CreateUser(inputUser);

            Assert.IsNotNull(user);
            Assert.AreEqual(expectUser, user);
        }

        [TestMethod]
        public async Task TestUpdateUser()
        {
            var inputUser = new User
            {
                Status = "active"
            };
            var expectUser = new User
            {
                Name = "Mock user 1",
                Email = $"mock-{DateTime.Now.Ticks}@mock.test",
                Gender = "male",
                Status = "active"
            };

            _mockApiService.Setup(x => x.UpdateUser(inputUser)).ReturnsAsync(expectUser);
            var userViewModel = new UserViewModel(_mockApiService.Object);
            var user = await userViewModel.UpdateUser(inputUser);

            Assert.IsNotNull(user);

            Assert.AreEqual("Mock user 1", user.Name);
            Assert.AreEqual("active", user.Status);
        }

        [TestMethod]
        public async Task TestDeleteUser()
        {
            _mockApiService.Setup(x => x.DeleteUser(1)).ReturnsAsync(true);
            var userViewModel = new UserViewModel(_mockApiService.Object);
            var success = await userViewModel.DeleteUser(1);

            Assert.IsTrue(success);
        }
    }
}
