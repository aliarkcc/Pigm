using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FollowListManager : IFollowListService
    {
        IFollowListDal _followList;

        public FollowListManager(IFollowListDal followList)
        {
            _followList = followList;
        }

        public IResult Add(FollowList entity)
        {
            _followList.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            _followList.Delete(id);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<FollowList>>> GetAll(Expression<Func<FollowList, bool>> filter = null)
        {
            
            if(filter==null)
            {
                List<FollowList> result = await _followList.GetAll();
                return new SuccessDataResult<List<FollowList>>(result, "Listed");
            }
            else
            {
                List<FollowList> result = await _followList.GetAll(filter);
                return  new SuccessDataResult<List<FollowList>>(result, "Listed");
            }
            
        }

        public IDataResult<FollowList> GetById(int id)
        {
            var result = _followList.Get(x => x.Id == id);
            return new SuccessDataResult<FollowList> (result, "Listed");
        }

        public IResult Update(FollowList entity)
        {
            _followList.Update(entity);
            return new SuccessResult();
        }
    }
}
