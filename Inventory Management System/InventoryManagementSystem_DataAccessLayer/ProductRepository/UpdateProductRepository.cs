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

namespace InventoryManagementSystem_DataAccessLayer.ProductRepository
{
    public class UpdateProductRepository: IUpdateRepository<ProductDTO>
    {
        private readonly string _connectionString;

        public UpdateProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }
        public async Task<bool> UpdateAsync(ProductDTO obj)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);

                rowsAffected = await connection.ExecuteAsync("SP_UpdatePerson", obj, commandType: CommandType.StoredProcedure);

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
