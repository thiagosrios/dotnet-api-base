﻿using ApiBase.Interfaces;
using ApiBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiBase.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Classe de serviço para controle de dados e operações para modelo de usuários
        /// </summary>
        /// <param name="userRepository">Repositório para consulta de informações de usuários</param>
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Retorna registro de usuário por login
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <returns>Objeto User</returns>
        public User FindUserByLogin(string login)
        {
            return this.userRepository.FindSingle(x => x.Login == login);
        }

        /// <summary>
        /// Retorna instância de usuário por id
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Registro de usuário por id</returns>
        public User GetUser(int id)
        {
            return this.userRepository.FindById(id);
        }

        /// <summary>
        /// Retorna lista de todos os usuários cadastrados, 
        /// permitindo modificação da lista antes de retornar resultado
        /// </summary>
        /// <returns>Objeto List com registro de usuários</returns>
        public List<User> GetUsers()
        {
            IEnumerable<User> query = this.userRepository.FindAll();
            List<User> users = query.Any() ? query.ToList() : new List<User>();

            return users;
        }

        /// <summary>
        /// Modifica objeto de usuário para criar hash 
        /// de senha antes de salvar registro no banco
        /// </summary>
        /// <param name="user">Objeto User</param>
        public void CreateUser(User user)
        {
            try
            {
                this.userRepository.Save(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar usuário: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualização de registro de usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="user">Objeto User</param>
        public void UpdateUser(int id, User user)
        {
            try
            {
                if (!this.userRepository.UserExists(id))
                {
                    throw new Exception("Usuário não encontrado");
                }

                this.userRepository.Update(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar usuário: " + ex.Message);
            }
        }

        /// <summary>
        /// Exclusão de registro de usuário
        /// </summary>
        /// <param name="user">Objeto User</param>
        public void DeleteUser(User user)
        {
            try
            {
                this.userRepository.Delete(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir usuário: " + ex.Message);
            }
        }
    }
}