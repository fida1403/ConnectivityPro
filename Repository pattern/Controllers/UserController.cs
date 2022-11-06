using BAL;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Repository_pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService;
        public UserController(IUserService userservice)
        {
            UserService = userservice;
        }


        [HttpGet("/{email}")]
        public ActionResult<Users> GetUserByEmail(string email)
        {
            var data=UserService.GetUserByEmail(email);
            return Ok(data);    
        }


        [HttpGet]
        public ActionResult <List<Users>> GetAllUser()
        {
            var data=UserService.GetAllUser();
            return Ok(data);
        }


        [HttpPost]
        public ActionResult<Users> CreateUser(Users obj)
        {
            var data = UserService.CreateUser(obj);
            return Ok("success");
        }


        [HttpPut]
        public ActionResult<Users> UpdateUser(Users obj)
        {
            var data = UserService.UpdateUser(obj);
            return Ok("success");
        }


        [HttpDelete]
        public ActionResult<Users> RemoveUser(string email)
        {
            var data = UserService.RemoveUser(email);
            return Ok("success");
        }
    }
}
