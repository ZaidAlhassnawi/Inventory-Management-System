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

namespace InventoryManagementSystem_DataAccessLayer.CategoreyRepository
{
    public class AddCategoryRepostiory : IAddRepository<CategoryDTO>
    {
        private readonly string _connectionString;

        public AddCategoryRepostiory(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        
        public async Task<int> AddAsync(CategoryDTO obj)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_Categories_Insert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CategoreName", obj.CategoreName);
                command.Parameters.AddWithValue("@Description", (object?)obj.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedDate", obj.CreatedDate);
              
                var outputIdParam = new SqlParameter("@NewCategoreID", SqlDbType.Int)
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
