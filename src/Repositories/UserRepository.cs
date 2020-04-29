﻿using ApiBase.Database;
using ApiBase.Interfaces;
using ApiBase.Models;

namespace ApiBase.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// Classe para consulta de usuários
        /// </summary>
        /// <param name="context">Instância do contexto</param>
        public UserRepository(Context context) : base(context){}

        /// <summary>
        /// Determina se um registro de usuário existe com base no id
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Valor da verificação de existência</returns>
        public bool UserExists(int id)
        {
            return this.Exists(x => x.Id == id);
        }
    }
}
