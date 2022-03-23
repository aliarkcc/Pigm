using AutoMapper;
using Business.Abstract;
using Core.Utilities.Result;
using Core.Utilities.Security.Token;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenService _tokenService;
        IMapper _mapper;
        public AuthManager(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<IDataResult<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.GetAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);
            if (user == null)
            {
                return new ErrorDataResult<UserDto>(null, "No");
            }
            else
            {
                if (user.Data.TokenExpireDate == null || String.IsNullOrEmpty(user.Data.Token))
                {
                    return await UpdateToken(user);
                }
                if (user.Data.TokenExpireDate < DateTime.Now)
                {
                    return await UpdateToken(user);
                }
            }
            return new SuccessDataResult<UserDto>(user.Data, "Ok");
        }

        private async Task<IDataResult<UserDto>> UpdateToken(IDataResult<UserDto> user)
        {
            var accessToken = _tokenService.CreateToken(user.Data.Id, user.Data.UserName);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user.Data);
            userUpdateDto.Token = accessToken.Token;
            userUpdateDto.TokenExpireDate = accessToken.Expiration;
            userUpdateDto.UpdatedUserId = user.Data.Id;
            var newUserUpdateDto = await _userService.UpdateAsync(userUpdateDto);
            var userDto = _mapper.Map<UserDto>(newUserUpdateDto.Data);
            return new SuccessDataResult<UserDto>(userDto, "Ok");
        }
    }
}
