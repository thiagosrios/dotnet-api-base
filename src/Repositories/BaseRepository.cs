using ApiBase.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiBase.Repositories
{
    /// <summary>
    /// Classe de base para os repositórios usados na aplicação
    /// </summary>
    /// <typeparam name="TEntity">Tipo de Entidade que herdará os métodos</typeparam>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public DbSet<TEntity> Model { get; }
        public DbContext Context { get; }

        /// <summary>
        /// Método construtor, injeção de contexto
        /// </summary>
        /// <param name="dbContext">Contexto utilizado para acesso ao model</param>
        public BaseRepository(DbContext context)
        {
            this.Context = context;
            this.Model = this.Context.Set<TEntity>();
        }

        /// <summary>
        /// Método genérico de busca por ID
        /// </summary>
        /// <param name="id">Código identificador da tabela</param>
        /// <returns>Registro do model usado no BaseService</returns>
        public TEntity FindById(object id)
        {
            return this.Model.Find(id);
        }

        /// <summary>
        /// Retorna Model para listar todos os registros da entidade
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll()
        {
            return this.Model;
        }

        /// <summary>
        /// Retorna Queryable da entidade a partir de expressão lambda com parâmetros de pesquisa
        /// </summary>
        /// <param name="predicate">Expressão lambda contendo filtro de pesquisa</param>
        /// <returns>IQueryable do model para ser manipulado</returns>
        public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Model.Where(predicate);
        }

        /// <summary>
        /// Retorna Lista da entidade a partir de expression para filtro de registros
        /// </summary>
        /// <param name="predicate">Expressão lambda contendo filtro de pesquisa</param>
        /// <param name="includes">Array de includes para entidades associadas</param>
        /// <returns>Lista da entidade, se houver resultados</returns>
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            List<TEntity> list = new List<TEntity>();
            IQueryable<TEntity> query = this.Search(predicate);
            if(query != null && query.Any())
            {
                query = this.BuildIncludeQuery(query, includes);
                list = query.AsNoTracking().ToList();
            }

            return list;
        }

        /// <summary>
        /// Retorna entidade a partir de expression e com include opcionais
        /// </summary>
        /// <param name="predicate">Expressão lambda contendo filtro de pesquisa</param>
        /// <param name="includes">Array de includes para entidades associadas</param>
        /// <returns>Registro da entidade encontrada</returns>
        public TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            TEntity entity = null;
            IQueryable<TEntity> query = this.Search(predicate);
            if(query != null && query.Any())
            {
                query = this.BuildIncludeQuery(query, includes);
                entity = query.SingleOrDefault();
            }

            return entity;
        }

        /// <summary>
        /// Adiciona entidades associadas na query a partir da lista de string com includes
        /// </summary>
        /// <param name="query">Queryable para concatenação de includes</param>
        /// <param name="includes">Array de string contendo os include para a tabela</param>
        /// <returns>IQueryable</returns>
        private IQueryable<TEntity> BuildIncludeQuery(IQueryable<TEntity> query, string[] includes)
        {
            if (includes != null && includes.Length > 0)
            {
                foreach(string include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        /// <summary>
        /// Salva registros no banco de dados a partir de objeto enviado
        /// </summary>
        /// <param name="data">Objeto da entidade que deve ser persistido</param>
        public virtual void Save(TEntity entity)
        {
            this.Model.Add(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Atualiza registros a partir de objeto enviado no parâmetro data
        /// </summary>
        /// <param name="entity">Objeto que deve ser atualizado no banco</param>
        /// <returns>Resultado da atualização, contendo quantidade de registros atualizados</returns>
        public virtual void Update(TEntity entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Model.Update(entity);
            this.Context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            this.Model.Remove(entity);
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Determina se um registro existe ou não com base no filtro informado
        /// </summary>
        /// <param name="predicate">Expressão lambda contendo filtro de pesquisa</param>
        /// <returns>Valor indicativo da existência do registro na base</returns>
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Model.Any(predicate); 
        }
    }
}
