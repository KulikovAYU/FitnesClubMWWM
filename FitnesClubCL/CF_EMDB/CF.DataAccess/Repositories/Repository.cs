using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FitnesClubCL.CF_EMDB.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext m_context;
        protected readonly IQueryable<TEntity> m_entities;

        public Repository(DbContext context)
        {
            m_context = context;
            m_entities = m_context.Set<TEntity>();
        }

        public TEntity Get(int Id)
        {
            return m_context?.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return m_entities?.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return m_entities.Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return m_entities.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            m_context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            m_context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            m_context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            m_context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
