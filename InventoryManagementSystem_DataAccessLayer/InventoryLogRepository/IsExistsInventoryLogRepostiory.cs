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
    public class IsExistsInventoryLogRepostiory : IIsExistsRepository<InventoryLog>
    {
        private readonly string _connectionString;

        public IsExistsInventoryLogRepostiory(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }

        public async Task<bool> IsExistsAsync(int ProductID) 
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_InventoryLog_IsExists", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductID", ProductID);

                await connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return result != null && Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
