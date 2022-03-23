using Core.Utilities.Result;
using Entities.Dtos.UserDtos;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<UserDto>> LoginAsync(LoginDto loginDto);
    }
}
