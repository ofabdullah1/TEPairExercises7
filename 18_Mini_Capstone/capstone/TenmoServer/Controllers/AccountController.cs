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
    public class AccountController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IUserDao userDao;
        private readonly IAccountDao accountDao;
        private readonly ITransferDao transferDao;
 


        public AccountController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao, IAccountDao _accountDao, ITransferDao _transferDao)
        {
            tokenGenerator = _tokenGenerator;
            userDao = _userDao;
            accountDao = _accountDao;
            transferDao = _transferDao;

        }

        [HttpGet("{userId}")]
        public ActionResult<Account> GetAccount(int userId)
        {

            
            Account recipientAccount = accountDao.GetAccount(userId);

            if (userId != 0)
            {
                return Ok(recipientAccount);
            }
            else
            {
                return NoContent();
            }

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

        [HttpPost]
        public ActionResult TransferMoney(Transfer transfer)
        {

            if (transfer.AccountFromId == transfer.AccountToId)
            {
                return BadRequest(new { message = "You cannot send money to yourself." });
            }
            else
            {
                return Ok(transferDao.MakeTransfer(transfer));
            }

        }
        [HttpGet("transfer")]
        public ActionResult<List<ReturnTransfer>> GetTransfers()
        {
            string username = User.Identity.Name;
            User user = userDao.GetUser(username);
            

            if (user.UserId != 0)
            {
                return Ok(transferDao.GetTransfers(user.UserId));
            }
            else
            {
                return NoContent();
            }

        }

    }
}
