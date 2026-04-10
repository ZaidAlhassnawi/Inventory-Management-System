using Dapper;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_DataAccessLayer.InventoryLogRepository
{
    public class GetInventoryLogByID : IGetByIDRepository<InventoryLog>
    {
        private readonly string _connectionString;

        public GetInventoryLogByID(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }

        public async Task<InventoryLog?> GetByIDAsync(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                return await connection.QueryFirstOrDefaultAsync<InventoryLog>(
                    "SP_InventoryLog_GetByID",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
