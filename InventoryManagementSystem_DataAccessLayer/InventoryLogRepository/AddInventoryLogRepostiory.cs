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
    public class AddInventoryLogRepostiory : IAddRepository<InventoryLog>
    {
        private readonly string _connectionString;

        public AddInventoryLogRepostiory(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        
        public async Task<int> AddAsync(InventoryLog obj)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_InventoryLog_Insert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductID", obj.ProductID);
                command.Parameters.AddWithValue("@LogType", obj.LogType);
                command.Parameters.AddWithValue("@Quantity", obj.Quantity);
                command.Parameters.AddWithValue("@PreviousStock", obj.PreviousStock);
                command.Parameters.AddWithValue("@NewStock", obj.NewStock);
                command.Parameters.AddWithValue("@Reason", (object)obj.Reason ?? DBNull.Value);
                command.Parameters.AddWithValue("@UserID", obj.UserID);

                if (obj.LogDate == DateTime.MinValue)
                    command.Parameters.AddWithValue("@LogDate", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@LogDate", obj.LogDate);

                var outputIdParam = new SqlParameter("@NewLogID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                await command.ExecuteNonQueryAsync();

                return outputIdParam.Value != DBNull.Value ? (int)outputIdParam.Value : -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
