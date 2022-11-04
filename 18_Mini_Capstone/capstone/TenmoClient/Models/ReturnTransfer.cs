using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoClient.Models
{
    public class ReturnTransfer
    {
        public int TransferId { get; set; }
        public int UserIdFrom { get; set; }
        public string UserFrom { get; set; }
        public int UserIdTo { get; set; }
        public string UserTo { get; set; }
        public decimal Amount { get; set; }

        public ReturnTransfer(int transferId, int userIdFrom, string userFrom, int userIdTo, string userTo, decimal amount)
        {
            TransferId = transferId;
            UserIdFrom = userIdFrom;
            UserFrom = userFrom;
            UserIdTo = userIdTo;
            UserTo = userTo;
            Amount = amount;
        }

        public ReturnTransfer()
        {

        }

    }
}
