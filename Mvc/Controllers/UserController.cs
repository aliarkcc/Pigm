using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private string url = "http://localhost:54694/api/";

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<ApiListResponseModel<UserDetailDto>>(url + "Users/GetList");
            return View(users.data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.GenderList = GenderFill();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            UserAddDto userAddDto = new UserAddDto()
            {
                UserName = userAddViewModel.UserName,
                FirstName = userAddViewModel.FirstName,
                LastName = userAddViewModel.LastName,
                Gender = userAddViewModel.GenderId == 1 ? true : false,
                Password = userAddViewModel.Password,
                Email = userAddViewModel.Email,
                Address = userAddViewModel.Address,
                DateOfBirth = userAddViewModel.DateOfBirth
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(url + "Users/Add", userAddDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<ApiResponseModel<UserUpdateDto>>(url + "Users/Get/" + id);
            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel()
            {
                Id = user.data.Id,
                Address = user.data.Address,
                DateOfBirth = user.data.DateOfBirth,
                Email = user.data.Email,
                FirstName = user.data.Email,
                GenderId = user.data.Gender == true ? 1 : 0,
                LastName = user.data.LastName,
                Password = user.data.Password,
                UserName = user.data.UserName
            };
            ViewBag.GenderList = GenderFill();
            return View(userUpdateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel userUpdateViewModel)
        {
            UserUpdateDto userUpdateDto = new UserUpdateDto()
            {
                Address = userUpdateViewModel.Address,
                DateOfBirth = userUpdateViewModel.DateOfBirth,
                Email = userUpdateViewModel.Email,
                FirstName = userUpdateViewModel.FirstName,
                Gender = userUpdateViewModel.GenderId == 1 ? true : false,
                Id = userUpdateViewModel.Id,
                LastName = userUpdateViewModel.LastName,
                Password = userUpdateViewModel.Password,
                UserName = userUpdateViewModel.UserName
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(url + "Users/Update", userUpdateDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<ApiResponseModel<UserDto>>(url + "Users/Get/" + id);
            UserDeleteViewModel userDeleteViewModel = new UserDeleteViewModel()
            {
                Id = user.data.Id,
                Address = user.data.Address,
                DateOfBirth = user.data.DateOfBirth,
                Email = user.data.Email,
                FirstName = user.data.Email,
                GenderName = user.data.Gender == true ? "Erkek" : "Kadın",
                LastName = user.data.LastName,
                Password = user.data.Password,
                UserName = user.data.UserName
            };
            ViewBag.GenderList = GenderFill();
            return View(userDeleteViewModel);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync(url + "Users/Delete/"+id);
            return RedirectToAction("Index");
        }
        private List<Gender> GenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender() { Id = 1, GenderName = "Erkek" });
            genders.Add(new Gender() { Id = 2, GenderName = "Kadın" });
            return genders;
        }

        private class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }
    }
}