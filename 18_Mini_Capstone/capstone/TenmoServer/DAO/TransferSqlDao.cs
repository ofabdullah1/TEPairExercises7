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


        public Transfer GetTransfer(int transferId)
        {
            Transfer returnTransfer = new Transfer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer_id, account_from ,account_to, amount FROM account WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);
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
