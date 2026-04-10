using Dapper;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_DataAccessLayer.InventoryLogRepository
{
    public class UpdateInventoryLogRepository: IUpdateRepository<InventoryLog>
    {
        private readonly string _connectionString;

        public UpdateInventoryLogRepository(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        public async Task<bool> UpdateAsync(InventoryLog obj)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_InventoryLog_Update", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductID", obj.ProductID);
                command.Parameters.AddWithValue("@LogType", obj.LogType);
                command.Parameters.AddWithValue("@Quantity", obj.Quantity);
                command.Parameters.AddWithValue("@PreviousStock", obj.PreviousStock);
                command.Parameters.AddWithValue("@NewStock", obj.NewStock);
                command.Parameters.AddWithValue("@Reason", (object)obj.Reason ?? DBNull.Value);
                command.Parameters.AddWithValue("@UserID", obj.UserID);
                command.Parameters.AddWithValue("@LogDate", obj.LogDate);

                rowsAffected = await command.ExecuteNonQueryAsync();

            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return (rowsAffected > 0);
        }
    }
}
