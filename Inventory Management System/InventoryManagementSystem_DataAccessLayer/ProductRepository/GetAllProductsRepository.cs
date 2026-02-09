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
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            SKU = reader.GetString(reader.GetOrdinal("SKU")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoreName")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                            Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                            CostPrice = reader.GetDecimal(reader.GetOrdinal("CostPrice")),
                            SellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")),
                            ImageURL = reader.GetString(reader.GetOrdinal("ImageURL")),
                            Description = reader.GetString(reader.GetOrdinal("Description"))

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
