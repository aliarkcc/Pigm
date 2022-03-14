using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IBaseRepository<T> where T:class,new()
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T,bool>>filter);
        T Add(T entity);
        bool Delete(int id);
        T Update(T entity);
    }
}
