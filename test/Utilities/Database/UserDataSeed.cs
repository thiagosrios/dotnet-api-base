using ApiBase.Database;
using ApiBase.Models;
using System;
using System.Collections.Generic;

namespace ApiBaseTest.Utilities.Database
{
    public class UserDataSeed : IDisposable
    {
        public Context Context { get; private set; } = new Context();

        public UserDataSeed()
        {
            List<User> users = new List<User>()
            {
                new User()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "test@mail.com",
                    Login = "test",
                    Password = "123456"
                },
                new User()
                {
                    FirstName = "Another",
                    LastName = "Test",
                    Email = "another@mail.com",
                    Login = "another",
                    Password = "123456"
                }
            };

            this.Context.AddRange(users);
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Database.EnsureDeleted();
            this.Context.Dispose();
        }
    }
}
