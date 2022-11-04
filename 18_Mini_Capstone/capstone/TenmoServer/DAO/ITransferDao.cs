using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoClient.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {

       Transfer MakeTransfer(Transfer transfer);
        List<ReturnTransfer> GetTransfers(int userId);

    }
}
