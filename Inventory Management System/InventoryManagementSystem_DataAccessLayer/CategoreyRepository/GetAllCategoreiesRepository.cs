using Dapper;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_DataAccessLayer.CategoreyRepository
{
    public class GetAllCategoreiesRepository: IGetAllRepository<CategoryDTO>
    {
        private readonly string _connectionString;

        public GetAllCategoreiesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var list = new List<CategoryDTO>();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                var result = await connection.QueryAsync<CategoryDTO>("SP_Categories_GetAll", commandType: CommandType.StoredProcedure);

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
