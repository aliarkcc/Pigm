using Core.Utilities.Result;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<UserDetailDto>>> GetAll();
        IDataResult<UserDto> Get(int id);
        IDataResult<UserDto> Add(UserAddDto userAddDto);
        IResult Delete(int id);
        IDataResult<UserUpdateDto> Update(UserUpdateDto userUpdateDto);
    }
}