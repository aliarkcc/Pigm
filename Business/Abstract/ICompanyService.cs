using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        Task<IDataResult<List<Company>>> GetAllAsync(Expression<Func<Company,bool>>filter=null);
        Task<IDataResult<Company>> GetByIdAsync(int id);
        Task<IResult> AddAsync(Company entity);
        Task<IResult> DeleteAsync(int id);
        Task<IResult> UpdateAsync(Company entity);
    }
}
