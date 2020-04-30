using ApiBase.Interfaces;
using ApiBase.Models;
using ApiBase.Filters;
using ApiBase.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            List<User> users = this.service.GetUsers();

            return Ok(users);
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = this.service.GetUser(id);
            if (user == null)
            {
                return NotFound(RequestResponse.ResourceNotFound("Usuário não encontrado"));
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            User newUser = this.service.CreateUser(user);

            return Ok(RequestResponse.RequestSucessful("Usuário criado com sucesso", newUser));
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public ActionResult<User> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest(RequestResponse.RequestFailed("Dados inválidos"));
            }

            this.service.UpdateUser(id, user);

            return Ok(RequestResponse.RequestSucessful("Usuário atualizado com sucesso!", user));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUsers(int id)
        {
            User user = this.service.GetUser(id);
            if (user == null)
            {
                return NotFound(RequestResponse.RequestFailed("Usuário não encontrado"));
            }

            this.service.DeleteUser(user);

            return Ok(RequestResponse.RequestSucessful("Usuário excluído"));
        }
    }
}
