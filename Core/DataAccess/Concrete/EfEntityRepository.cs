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
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (TContext db= new TContext())
            {
                await db.Set<TEntity>().AddAsync(entity);
                await db.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (TContext db= new TContext())
            {
                var deleteEntity = await db.Set<TEntity>().FindAsync(id);
                db.Set<TEntity>().Remove(deleteEntity);
                var data = await db.SaveChangesAsync();
                if (data > 0)
                    return true;
                return false;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext db= new TContext())
            {
                return await db.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext db= new TContext())
            {
                return filter == null
                    ? await db.Set<TEntity>().ToListAsync()
                    : await db.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (TContext db= new TContext())
            {
                db.Set<TEntity>().Update(entity);
                await db.SaveChangesAsync();
                return entity;
            }
        }
    }
}
