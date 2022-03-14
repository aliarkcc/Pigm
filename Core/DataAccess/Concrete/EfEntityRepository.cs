using Core.DataAccess.Abstract;
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public class EfEntityRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity:class,IEntity,new()
        where TContext:DbContext,new()
    {
        public TEntity Add(TEntity entity)
        {
            using (TContext db= new TContext())
            {
                var addedEntity = db.Entry(entity);
                addedEntity.State = EntityState.Added;
                db.SaveChanges();
                return entity;
            }
        }

        public bool Delete(int id)
        {
            using (TContext db= new TContext())
            {
                var deletedEntity = db.Set<TEntity>().Find(id);
                db.Set<TEntity>().Remove(deletedEntity);
                var data = db.SaveChanges();
                if (data > 0)
                    return true;
                return false;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext db= new TContext())
            {
                return db.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext db= new TContext())
            {
                return filter == null
                    ? await db.Set<TEntity>().ToListAsync()
                    : await db.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (TContext db= new TContext())
            {
                var updatedEntity = db.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                db.SaveChanges();
                return entity;
            }
        }
    }
}
