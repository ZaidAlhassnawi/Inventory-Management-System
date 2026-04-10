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

namespace InventoryManagementSystem_DataAccessLayer.CategoreyRepository
{
    public class UpdateCategoryRepository: IUpdateRepository<CategoryDTO>
    {
        private readonly string _connectionString;

        public UpdateCategoryRepository(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        public async Task<bool> UpdateAsync(CategoryDTO obj)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_Categories_Update", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CategoryID", obj.CategoryID);
                command.Parameters.AddWithValue("@CategoreName", obj.CategoreName);
                command.Parameters.AddWithValue("@Description", obj.Description);
                command.Parameters.AddWithValue("@CreatedDate", obj.CreatedDate);

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
