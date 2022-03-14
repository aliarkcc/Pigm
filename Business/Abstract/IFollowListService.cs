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
        Task<IDataResult<List<FollowList>>> GetAll(Expression<Func<FollowList,bool>>filter=null);
        IDataResult<FollowList> GetById(int id);
        IResult Add(FollowList entity);
        IResult Delete(int id);
        IResult Update(FollowList entity);
    }
}
