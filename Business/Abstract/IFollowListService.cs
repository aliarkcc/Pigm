using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFollowListService
    {
        Task<IDataResult<List<FollowList>>> GetAllAsync(Expression<Func<FollowList,bool>>filter=null);
        Task<IDataResult<FollowList>> GetByIdAsync(int id);
        Task<IResult> AddAsync(FollowList entity);
        Task<IResult> DeleteAsync(int id);
        Task<IResult> UpdateAsync(FollowList entity);
    }
}
