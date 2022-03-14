using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<UserDto> Add(UserAddDto userAddDto)
        {
            var user = new User()
            {
                UserName = userAddDto.UserName,
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName,
                Address = userAddDto.Address,
                Email = userAddDto.Email,
                Gender = userAddDto.Gender,
                DateOfBirth = userAddDto.DateOfBirth,
                Password = userAddDto.Password,
                CreatedUserId = 1,
                CreatedDate = DateTime.Now,
            };
            var userAdd= _userDal.Add(user);
            UserDto userDto = new UserDto()
            {
                Address = userAdd.Address,
                DateOfBirth = userAdd.DateOfBirth,
                Email = userAdd.Email,
                FirstName = userAdd.FirstName,
                Gender = userAdd.Gender,
                LastName = userAdd.LastName,
                UserName = userAdd.UserName,
                Id=userAdd.Id
            };

            return new SuccessDataResult<UserDto>(userDto,"Ok");
        }

        public IResult Delete(int id)
        {
            var response= _userDal.Delete(id);
            if (response)
                return new SuccessResult();
            return new ErrorResult();
        }

        public IDataResult<UserDto> Get(int id)
        {
            var user = _userDal.Get(x => x.Id == id);
            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender
            };
            return new SuccessDataResult<UserDto>(userDto, "Ok");
        }

        public async Task<IDataResult<List<UserDetailDto>>> GetAll()
        {
            List<UserDetailDto> userDetailDtos = new List<UserDetailDto>();
            var response =await _userDal.GetAll();
            foreach (var item in response.ToList())
            {
                userDetailDtos.Add(new UserDetailDto()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Address = item.Address,
                    DateOfBirth = item.DateOfBirth,
                    Gender = item.Gender == true ? "Erkek" : "Kadın"
                });
            }
            return new SuccessDataResult<List<UserDetailDto>>(userDetailDtos, "Ok");
        }

        public IDataResult<UserUpdateDto> Update(UserUpdateDto userUpdateDto)
        {
            var user = _userDal.Get(x=>x.Id==userUpdateDto.Id);
            var user1 = new User()
            {
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,
                FirstName = user.Email,
                LastName = user.LastName,
                Gender = user.Gender,
                Password = user.Password,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth,
                UpdatedUserId = 1,
                UpdatedDate = DateTime.Now,
                CreatedDate=user.CreatedDate,
                CreatedUserId=user.CreatedUserId
            };
            var userUpdate = _userDal.Update(user1);
            UserUpdateDto newUserUpdateDto = new UserUpdateDto()
            {
                Id = userUpdate.Id,
                Address = userUpdate.Address,
                Email = userUpdate.Email,
                FirstName = userUpdate.Email,
                LastName = userUpdate.LastName,
                Gender = userUpdate.Gender,
                Password = userUpdate.Password,
                UserName = userUpdate.UserName,
                DateOfBirth = userUpdate.DateOfBirth,
            };
            return new SuccessDataResult<UserUpdateDto>(newUserUpdateDto, "Ok");
        }
    }
}
