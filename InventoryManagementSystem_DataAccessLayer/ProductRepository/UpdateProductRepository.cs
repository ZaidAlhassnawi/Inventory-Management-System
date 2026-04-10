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

namespace InventoryManagementSystem_DataAccessLayer.ProductRepository
{
    public class UpdateProductRepository: IUpdateRepository<ProductDTO>
    {
        private readonly string _connectionString;

        public UpdateProductRepository(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        public async Task<bool> UpdateAsync(ProductDTO obj)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("sp_Products_Update", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductID", obj.ProductID);
                command.Parameters.AddWithValue("@ProductName", obj.ProductName);
                command.Parameters.AddWithValue("@SKU", obj.SKU);
                command.Parameters.AddWithValue("@CategoryID", obj.CategoryID );
                command.Parameters.AddWithValue("@SupplierID", obj.SupplierID );

                command.Parameters.AddWithValue("@Stock", (object?)obj.Stock ?? DBNull.Value);
                command.Parameters.AddWithValue("@SellingPrice", (object?)obj.SellingPrice ?? DBNull.Value);
                command.Parameters.AddWithValue("@CostPrice", (object?)obj.CostPrice ?? DBNull.Value);
                command.Parameters.AddWithValue("@ImageURL", (object?)obj.ImageURL ?? DBNull.Value);
                command.Parameters.AddWithValue("@Description", (object?)obj.Description ?? DBNull.Value);

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
