using Core.Utilities.Result;
using Core.Utilities.Security.Token;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<UserDetailDto>>> GetAllAsync(Expression<Func<User, bool>> filter = null);
        Task<IDataResult<UserDto>> GetAsync(Expression<Func<User, bool>> filter);
        Task<IDataResult<UserDto>> GetByIdAsync(int id);
        Task<IDataResult<UserDto>> AddAsync(UserAddDto userAddDto);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<UserUpdateDto>> UpdateAsync(UserUpdateDto userUpdateDto);
        Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto);
    }
}