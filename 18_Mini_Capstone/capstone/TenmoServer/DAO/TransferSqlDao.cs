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




    }
}
