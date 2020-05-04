using ApiBase.Controllers;
using ApiBase.Exceptions;
using ApiBase.Models;
using ApiBase.Repositories;
using ApiBase.Services;
using ApiBaseTest.Utilities;
using ApiBaseTest.Utilities.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ApiBaseTest.UnitTests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController Controller { get; set; }
        private UserService Service { get; set; }
        private UserRepository Repository { get; set; }
        private UserDataSeed Seed { get; set; }

        [TestInitialize]
        public void Setup()
        {
            this.Repository = new UserRepository();
            this.Service = new UserService(this.Repository);
            this.Controller = new UserController(this.Service);
            this.Seed = new UserDataSeed();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.Seed.Dispose();
        }

        [TestMethod]
        public void TestGetUser()
        { 
            var result = this.Controller.GetUser(1);

            Assert.That.RequestIsSuccessful(result);
        }

        [TestMethod]
        public void TestGetUserNotFound()
        {
            var result = this.Controller.GetUser(10);

            Assert.That.RequestResourceNotFound(result);
        }

        [TestMethod]
        public void TestGetUsers()
        {
            var result = this.Controller.GetUsers();

            Assert.That.RequestIsSuccessful(result);
        }

        [TestMethod]
        public void TestPostUser()
        {
            User user = new User() 
            {
                FirstName = "Test 2",
                LastName = "Test 2",
                Email = "test2@mail.com",
                Login = "test2",
                Password = "123456"
            };
            var result = this.Controller.PostUser(user);

            Assert.That.RequestIsSuccessful(result);
        }

        [TestMethod]
        public void TestPostUserException()
        {
            BusinessException exception = Assert.ThrowsException<BusinessException>(
                () => this.Controller.PostUser(null)
            );

            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void TestPutUser()
        {
            User user = this.Repository.FindById(1);
            user.Login = "login";
            var result = this.Controller.PutUser(1, user);

            Assert.That.RequestIsSuccessful(result);
        }

        [TestMethod]
        public void TestPutUserFail()
        {
            User user = this.Repository.FindById(1);
            user.Login = "login";
            var result = this.Controller.PutUser(2, user);

            Assert.That.RequestFailed(result);
        }

        [TestMethod]
        public void TestPutUserException()
        {
            NullReferenceException exception = Assert.ThrowsException<NullReferenceException>(
                () => this.Controller.PutUser(1, null)
            );

            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            var result = this.Controller.DeleteUser(1);

            Assert.That.RequestIsSuccessful(result);
        }

        [TestMethod]
        public void TestDeleteUserNotFound()
        {
            var result = this.Controller.DeleteUser(0);

            Assert.That.RequestResourceNotFound(result);
        }
    }
}
