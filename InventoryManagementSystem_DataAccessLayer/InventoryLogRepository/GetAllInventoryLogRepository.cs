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
    public class GetAllInventoryLogRepository: IGetAllRepository<InventoryLog>
    {
        private readonly string _connectionString;

        public GetAllInventoryLogRepository(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }

        public async Task<List<InventoryLog>> GetAllAsync()
        {
            var list = new List<InventoryLog>();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                var result = await connection.QueryAsync<InventoryLog>("SP_InventoryLog_GetAll", commandType: CommandType.StoredProcedure);

                list = result.ToList();
            }
            catch (SqlException ex)
            {

            }
            catch (Exception ex)
            {

            }
            return list;
        }
    }
}
