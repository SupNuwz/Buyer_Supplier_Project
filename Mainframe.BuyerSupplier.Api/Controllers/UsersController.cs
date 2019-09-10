using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Core.Dto;
using Mainframe.BuyerSupplier.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mainframe.BuyerSupplier.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private IUserBusinessEntity userService;
        public UsersController(IUserBusinessEntity userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public int AddUser([FromBody]UserDto value)
        {
            return userService.AddUser(value);             
        }

        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return this.userService.GetUsers();
        }
        [HttpGet("{id}")]
        public UserDto GetUser(int id)
        {
            return userService.GetUser(id);
        }

        [HttpGet("initialuserdata/{userId}")]
        public UserCreationInitialDataDto GetInitialUserCeationData(int userId)
        {
            return userService.GetCreationUserCreationInitialData(userId);
        }


        [HttpDelete("{id}")]
        public IEnumerable<UserDto> DeleteUser(int id)
        {
            userService.DeleteUser(id);
            return this.userService.GetUsers();
        }

        [HttpPut]
        public void UpdateUser([FromBody]UserDto value)
        {
            userService.UpdateUser(value);
        }

        [HttpGet("userType/{userType}")]
        public IEnumerable<UserDto> GetUsersByType(string userType)
        {
            var users =  this.userService.GetUsers();
            var selUsers = users.Where(r => r.UserType == userType);
            return selUsers;
        }
    }
}