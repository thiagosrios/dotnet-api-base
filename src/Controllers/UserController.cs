﻿using ApiBase.Interfaces;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> users = this.repository.GetAll();

            return Ok(users);
        }

        // GET: api/Users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = this.repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/1
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                this.repository.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.repository.UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            this.repository.Save(user);

            return Ok(user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUsers(int id)
        {
            User user = this.repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            this.repository.Delete(user);

            return user;
        }
    }
}
