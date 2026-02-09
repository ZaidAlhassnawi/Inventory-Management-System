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
    public class AddProductRepostiory : IAddRepository<ProductDTO>
    {
        private readonly string _connectionString;

        public AddProductRepostiory(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        
        public async Task<int> AddAsync(ProductDTO obj)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_Products_Insert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductName", obj.ProductName);
                command.Parameters.AddWithValue("@SKU", obj.SKU);
                command.Parameters.AddWithValue("@CategoryID", obj.CategoryID);
                command.Parameters.AddWithValue("@SupplierID", obj.SupplierID);
                command.Parameters.AddWithValue("@Stock", obj.Stock);
                command.Parameters.AddWithValue("@CostPrice", obj.CostPrice);
                command.Parameters.AddWithValue("@SellingPrice", obj.SellingPrice);
                command.Parameters.AddWithValue("@ImageURL", obj.ImageURL?? "");
                command.Parameters.AddWithValue("@Description", obj.Description?? "");



                var outputIdParam = new SqlParameter("@NewProductID", SqlDbType.Int)
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
