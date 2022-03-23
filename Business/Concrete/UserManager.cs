using AutoMapper;
using Business.Abstract;
using Core.Utilities.Result;
using Core.Utilities.Security.Token;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly AppSettings _appSettings;
        private IMapper _mapper;
        public UserManager(IUserDal userDal, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userDal = userDal;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<IDataResult<UserDto>> AddAsync(UserAddDto userAddDto)
        {
            var user = _mapper.Map<User>(userAddDto);
            user.CreatedDate = DateTime.Now;
            user.CreatedUserId = 1;
            var userAdd = await _userDal.AddAsync(user);
            var userDto = _mapper.Map<UserDto>(userAdd);
            return new SuccessDataResult<UserDto>(userDto, "Ok");
            //var user = new User()
            //{
            //    UserName = userAddDto.UserName,
            //    FirstName = userAddDto.FirstName,
            //    LastName = userAddDto.LastName,
            //    Address = userAddDto.Address,
            //    Email = userAddDto.Email,
            //    Gender = userAddDto.Gender,
            //    DateOfBirth = userAddDto.DateOfBirth,
            //    Password = userAddDto.Password,
            //    CreatedUserId = 1,
            //    CreatedDate = DateTime.Now,
            //};
            //var userAdd = await _userDal.AddAsync(user);
            //UserDto userDto = new UserDto()
            //{
            //    Address = userAdd.Address,
            //    DateOfBirth = userAdd.DateOfBirth,
            //    Email = userAdd.Email,
            //    FirstName = userAdd.FirstName,
            //    Gender = userAdd.Gender,
            //    LastName = userAdd.LastName,
            //    UserName = userAdd.UserName,
            //    Id = userAdd.Id
            //};

            //return new SuccessDataResult<UserDto>(userDto, "Ok");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var response = await _userDal.DeleteAsync(id);
            if (response)
                return new SuccessResult();
            return new ErrorResult();
        }

        public async Task<IDataResult<UserDto>> GetAsync(Expression<Func<User, bool>> filter)
        {
            var user = await _userDal.GetAsync(filter);
            if (user!=null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new SuccessDataResult<UserDto>(userDto, "Ok");
            }
            return new ErrorDataResult<UserDto>(null, "No");
            //UserDto userDto = new UserDto()
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Address = user.Address,
            //    Email = user.Email,
            //    DateOfBirth = user.DateOfBirth,
            //    Gender = user.Gender
            //};
            //return new SuccessDataResult<UserDto>(userDto, "Ok");
        }

        public async Task<IDataResult<List<UserDetailDto>>> GetAllAsync(Expression<Func<User, bool>> filter = null)
        {
            if (filter == null)
            {
                var response = await _userDal.GetAllAsync();
                var userDetailDtos = _mapper.Map<List<UserDetailDto>>(response);
                return new SuccessDataResult<List<UserDetailDto>>(userDetailDtos, "Ok");
            }
            else
            {
                var response = await _userDal.GetAllAsync(filter);
                var userDetailDtos = _mapper.Map<List<UserDetailDto>>(response);
                return new SuccessDataResult<List<UserDetailDto>>(userDetailDtos, "Ok");
            }
            //foreach (var item in response.ToList())
            //{
            //    userDetailDtos.Add(new UserDetailDto()
            //    {
            //        Id = item.Id,
            //        UserName = item.UserName,
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        Email = item.Email,
            //        Address = item.Address,
            //        DateOfBirth = item.DateOfBirth,
            //        Gender = item.Gender == true ? "Erkek" : "Kadın"
            //    });
        }

        public async Task<IDataResult<UserUpdateDto>> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var getUser = await _userDal.GetAsync(x => x.Id == userUpdateDto.Id);
            var user=_mapper.Map<User>(userUpdateDto);
            user.CreatedDate = getUser.CreatedDate;
            user.CreatedUserId = getUser.CreatedUserId;
            user.UpdatedDate = DateTime.Now;
            user.UpdatedUserId = 1;
            user.Token = userUpdateDto.Token;
            user.TokenExpireDate = userUpdateDto.TokenExpireDate;
            var resultUpdate = await _userDal.UpdateAsync(user);
            var  userUpdateMap= _mapper.Map<UserUpdateDto>(resultUpdate);
            return new SuccessDataResult<UserUpdateDto>(userUpdateMap, "Ok");
            //var user1 = new User()
            //{
            //    Id = user.Id,
            //    Address = userUpdateDto.Address,
            //    Email = userUpdateDto.Email,
            //    FirstName = userUpdateDto.Email,
            //    LastName = userUpdateDto.LastName,
            //    Gender = userUpdateDto.Gender,
            //    Password = userUpdateDto.Password,
            //    UserName = userUpdateDto.UserName,
            //    DateOfBirth = userUpdateDto.DateOfBirth,
            //    UpdatedUserId = 1,
            //    UpdatedDate = DateTime.Now,
            //    CreatedDate = user.CreatedDate,
            //    CreatedUserId = user.CreatedUserId
            //};

            //UserUpdateDto newUserUpdateDto = new UserUpdateDto()
            //{
            //    Id = userUpdate.Id,
            //    Address = userUpdate.Address,
            //    Email = userUpdate.Email,
            //    FirstName = userUpdate.Email,
            //    LastName = userUpdate.LastName,
            //    Gender = userUpdate.Gender,
            //    Password = userUpdate.Password,
            //    UserName = userUpdate.UserName,
            //    DateOfBirth = userUpdate.DateOfBirth,
            //};

        }

        public async Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto)
        {
            var user = await _userDal.GetAsync(x => x.UserName == userForLoginDto.UserName && x.Password == userForLoginDto.Password);
            if (user == null)
                return null;

        }

        public async Task<IDataResult<UserDto>> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new SuccessDataResult<UserDto>(userDto, "Ok");
            }
            return new ErrorDataResult<UserDto>(null, "No");
        }
    }
}