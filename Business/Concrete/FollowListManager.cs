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

        public async Task<IResult> AddAsync(FollowList entity)
        {
            await _followList.AddAsync(entity);
            return new SuccessResult();
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            await _followList.DeleteAsync(id);
            return new SuccessResult();
        }

        public async Task<IDataResult<List<FollowList>>> GetAllAsync(Expression<Func<FollowList, bool>> filter = null)
        {
            
            if(filter==null)
            {
                var result = await _followList.GetAllAsync();
                return new SuccessDataResult<List<FollowList>>(result, "Listed");
            }
            else
            {
                var result = await _followList.GetAllAsync(filter);
                return  new SuccessDataResult<List<FollowList>>(result, "Listed");
            }
            
        }

        public async Task<IDataResult<FollowList>> GetByIdAsync(int id)
        {
            var result =await _followList.GetAsync(x => x.Id == id);
            if (result!=null)
            {
                return new SuccessDataResult<FollowList>(result, "Listed");
            }
            return new ErrorDataResult<FollowList>(result, "Listed");
        }

        public async Task<IResult> UpdateAsync(FollowList entity)
        {
            await _followList.UpdateAsync(entity);
            return new SuccessResult();
        }
    }
}
