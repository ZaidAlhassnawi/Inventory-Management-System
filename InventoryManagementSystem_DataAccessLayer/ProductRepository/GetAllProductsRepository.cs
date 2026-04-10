using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_DataAccessLayer.ProductRepository
{
    public class GetAllProductsRepository :IGetAllRepository<ProductDTO>
    {
        private readonly string _connectionString;

        public GetAllProductsRepository(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var list = new List<ProductDTO>();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();

                using var command = new SqlCommand("SP_Products_GetAll", connection);

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    // التكرار بشكل غير متزامن
                    while (await reader.ReadAsync())
                    {
                        list.Add(new ProductDTO
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),

                            ProductName = reader.IsDBNull(reader.GetOrdinal("ProductName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ProductName")),
                            SKU = reader.IsDBNull(reader.GetOrdinal("SKU")) ? string.Empty : reader.GetString(reader.GetOrdinal("SKU")),

                            CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoreName")) ? null : reader.GetString(reader.GetOrdinal("CategoreName")),
                            SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader.GetString(reader.GetOrdinal("SupplierName")),

                            Stock = reader.IsDBNull(reader.GetOrdinal("Stock")) ? null : reader.GetInt32(reader.GetOrdinal("Stock")),


                            CostPrice = reader.GetDecimal(reader.GetOrdinal("CostPrice")),
                            SellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")),

                            ImageURL = reader.IsDBNull(reader.GetOrdinal("ImageURL")) ? null : reader.GetString(reader.GetOrdinal("ImageURL")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),

                        });
                    }
                }

            }
            catch (SqlException ex)
            {

            }
            catch (Exception ex)
            {
                // سجل الخطأ هنا
            }
            return list;
        }
    }
}
