using ApiBase.Exceptions;
using ApiBase.Models;
using ApiBase.Repositories;
using ApiBase.Services;
using ApiBaseTest.Utilities.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ApiBaseTest.Utilities;
using System.Linq;

namespace ApiBaseTest.UnitTests.Service
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService Service { get; set; }
        private UserRepository Repository { get; set; }
        private UserDataSeed Seed { get; set; }

        [TestInitialize]
        public void Setup()
        {
            this.Seed = new UserDataSeed();
            this.Repository = new UserRepository();
            this.Service = new UserService(this.Repository);
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.Seed.Dispose();
        }

        [TestMethod]
        public void TestCreateUser()
        {
            User newUser = new User()
            {
                FirstName = "Test 2",
                LastName = "Test 2",
                Email = "test2@mail.com",
                Login = "test2",
                Password = "123456"
            }; 

            User user = this.Service.CreateUser(newUser);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestCreateUserExcpetion()
        {
            BusinessException exception = Assert.ThrowsException<BusinessException>(
                () => this.Service.CreateUser(null)
            );

            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void TestFindByLogin()
        {
            User result = this.Service.FindUserByLogin("test");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetUser()
        {
            User result = this.Service.GetUser(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetUsers()
        {
            List<User> result = this.Service.GetUsers();

            Assert.That.IsNotNullAndIsList(result);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            User user = this.Repository.FindById(1);
            user.Login = "login";

            this.Service.UpdateUser(1, user);

            Assert.IsNotNull(user.Login);
        }

        [TestMethod]
        public void TestUpdateUserExcpetion()
        {
            BusinessException exception = Assert.ThrowsException<BusinessException>(
                () => this.Service.UpdateUser(1, null)
            );

            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            User user = this.Repository.FindById(1);
            this.Service.DeleteUser(user);
            User deleted = this.Repository.FindById(1);

            Assert.IsNull(deleted);
            Assert.AreEqual(this.Repository.Model.Count(), 1);
        }

        [TestMethod]
        public void TestDeleteUserExcpetion()
        {
            BusinessException exception = Assert.ThrowsException<BusinessException>(
                () => this.Service.DeleteUser(null)
            );

            Assert.IsNotNull(exception);
        }
    }
}
