using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoClient.Models;
using System.Collections.Generic;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IUserDao userDao;
        private readonly IAccountDao accountDao;


        public UserController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao, IAccountDao _accountDao)
        {
            tokenGenerator = _tokenGenerator;
            userDao = _userDao;
            accountDao = _accountDao;

        }



        [HttpGet]
        public ActionResult<List<User>> GetSendableUsers()
        {
            string username = User.Identity.Name;
            User user = userDao.GetUser(username);



         return Ok(userDao.GetUsersExceptLogged(user.UserId));

        }






    }
}





