using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoClient.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }

        public Account(int accountid, int userid, decimal balance)
        {
            AccountId = accountid;
            UserId = userid;
            Balance = balance;
        }

        public Account()
        {

        }


        public override string ToString()
        {
            return $"{Balance}";
        }

    }
}
