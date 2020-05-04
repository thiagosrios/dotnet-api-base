using ApiBase.Models;
using ApiBase.Repositories;
using ApiBaseTest.Utilities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ApiBaseTest.UnitTests.Repositories
{
    [TestClass]
    public class BaseRepositoryTest
    {
        private BaseRepository<User> Repository { get; set; }
        public UserDataSeed Seed { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Seed = new UserDataSeed();
            Repository = new UserRepository();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.Seed.Dispose();
        }

        [TestMethod]
        public void TestFindById()
        {
            User result = this.Repository.FindById(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestFindAll()
        {
            IEnumerable<User> users = this.Repository.FindAll();

            Assert.IsNotNull(users);
            Assert.AreEqual(users.ToList().Count, 2);
        }

        [TestMethod]
        public void TestFindSingle()
        {
            User result = this.Repository.FindSingle(x => x.Id == 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestFindList()
        {
            List<User> result = this.Repository.FindList(x =>
                EF.Functions.Like(x.LastName, "%Test")
            );

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 1);
        }

        [TestMethod]
        public void TestSave()
        {
            User user = new User()
            {
                FirstName = "Test 2",
                LastName = "Test 2",
                Email = "test2@mail.com",
                Login = "test2",
                Password = "123456"
            };

            this.Repository.Save(user);

            Assert.IsNotNull(user.Id);
        }

        [TestMethod]
        public void TestUpdate()
        {
            User user = this.Repository.FindById(1);
            user.LastName = "Test Update";

            this.Repository.Update(user);

            Assert.IsNotNull(user.LastName);
        }

        [TestMethod]
        public void TestDelete()
        {
            User user = this.Repository.FindById(1);
            this.Repository.Delete(user);
            User deleted = this.Repository.FindById(1);

            Assert.IsNull(deleted);
            Assert.AreEqual(this.Repository.Model.Count(), 1);
        }

        [TestMethod]
        public void TestExists()
        {
            bool result = this.Repository.Exists(x => x.Id == 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSearch()
        {
            IQueryable<User> result = this.Repository.Search(x => x.Email == "test@mail.com");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }
    }
}
