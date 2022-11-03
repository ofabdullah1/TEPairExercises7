using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoClient.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {

       Account GetAccount(int accountId);

    }
}
