using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int TransferStatus { get; set; }
        public int TransferTypeId { get; set; }
        public int AccountFromId { get; set; }
        public int AccountToId { get; set; }
        public decimal Amount { get; set; }

        public Transfer(int transferId, int transferStatusId, int transferTypeId, int accountFromId, int accountToId, decimal amount)
        {
            TransferId = transferId;
            TransferStatus = transferStatusId;
            TransferTypeId = transferTypeId;
            AccountFromId = accountFromId;
            AccountToId = accountFromId;
            Amount = amount;
        }

        public Transfer()
        {

        }

    }
}
