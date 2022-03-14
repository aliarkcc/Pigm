using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<IActionResult> Index()
        {
            var result =await _userService.GetAll();
            if (result.Success)
                return View(result.Data);
            return BadRequest();
        }
    }
}
