using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoClient.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {
        private readonly string connectionString;



        public TransferSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        public Transfer MakeTransfer(Transfer transfer)
        {
            Transfer returnTransfer = new Transfer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION; UPDATE account SET balance = balance - @amount WHERE account_id = @accountFrom; UPDATE account SET balance = balance + @amount WHERE account_id = @accountTo; " +
                                                    "INSERT INTO transfer (account_from,account_to,amount,transfer_status_id,transfer_type_id) VALUES (@accountFrom, @accountTo, @amount, 1, 1); COMMIT; ", conn);
                    cmd.Parameters.AddWithValue("@accountFrom", transfer.AccountFromId);
                    cmd.Parameters.AddWithValue("@accountTo", transfer.AccountToId);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnTransfer = GetTransferFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnTransfer;
        }

        public List<ReturnTransfer> GetTransfers(int userId)
        {
            List<ReturnTransfer> transfers = new List<ReturnTransfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT transfer.transfer_id, userFrom.user_id AS user_id_from, userFrom.username" +
                        " AS user_from, userTo.user_id AS user_id_to, userTo.username AS user_to, transfer.amount " +
                        "FROM transfer " +
                        "JOIN account AS acctFrom ON transfer.account_from = acctFrom.account_id " +
                        "JOIN tenmo_user AS userFrom " +
                        "ON acctFrom.user_id = userFrom.user_id " +
                        "JOIN account AS acctTo ON transfer.account_to = acctTo.account_id " +
                        "JOIN tenmo_user AS userTo ON acctTo.user_id = userTo.user_id " +
                        "WHERE userFrom.user_id = @user_to OR userTo.user_id = @user_from;", conn);
                    cmd.Parameters.AddWithValue("@user_to", userId);
                    cmd.Parameters.AddWithValue("@user_from", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        ReturnTransfer transfer = GetReturnTransferFromReader(reader);
                        transfers.Add(transfer);
                    }
                }
            }
            catch (Exception)
            {
                transfers = new List<ReturnTransfer>();
            }
            return transfers;
        }

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer a = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferStatus = Convert.ToInt32(reader["transfer_status_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                AccountFromId = Convert.ToInt32(reader["account_from_id"]),
                AccountToId = Convert.ToInt32(reader["account_to_id"]),
                Amount = Convert.ToDecimal(reader["amount"]),

            };

            return a;
        }
        private ReturnTransfer GetReturnTransferFromReader(SqlDataReader reader)
        {
            ReturnTransfer a = new ReturnTransfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                UserIdFrom = Convert.ToInt32(reader["user_id_from"]),
                UserFrom = Convert.ToString(reader["user_from"]),
                UserIdTo = Convert.ToInt32(reader["user_id_to"]),
                UserTo = Convert.ToString(reader["user_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),

            };

            return a;
        }



    }
}
