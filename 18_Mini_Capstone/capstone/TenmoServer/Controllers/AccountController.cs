using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoClient.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IUserDao userDao;
        private readonly IAccountDao accountDao;
       

        public AccountController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao, IAccountDao _accountDao)
        {
            tokenGenerator = _tokenGenerator;
            userDao = _userDao;
            accountDao = _accountDao;
           
        }



        [HttpGet]
        public ActionResult<Account> GetAccount()
        {

            string username = User.Identity.Name;
            User user = userDao.GetUser(username);
            Account account = accountDao.GetAccount(user.UserId);

            if (!string.IsNullOrEmpty(username))
            {
                return Ok(account);
            }
            else
            {
                return NoContent();
            }

        }
        //[HttpGet]
        //public ActionResult<List<Account>> GetAllAccounts()
        //{




        //    return Response.data;
        //}

    //    [HttpGet]



    //}
}




}
