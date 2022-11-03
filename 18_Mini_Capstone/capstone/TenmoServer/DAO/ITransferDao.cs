using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoClient.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {

       Transfer GetTransfer(int accountFrom, int accountTo, decimal amount);

    }
}
