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
    public class DeleteProductRepository:IDeleteRepository<ProductDTO>
    {
        private readonly string _connectionString;

        public DeleteProductRepository(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }

        public async Task<bool> DelteAsync(int ID)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);
                rowsAffected = await connection.ExecuteAsync("SP_Suppliers_Delete", new { Id = ID });

            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                
            }

            return (rowsAffected > 0);
        }
    }
}
