using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result=await _userService.GetAll();
            return Ok(result.Data);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetLis1t()
        {
            var result = await _userService.GetAll();
            return Ok(result.Data);
        }
    }
}
