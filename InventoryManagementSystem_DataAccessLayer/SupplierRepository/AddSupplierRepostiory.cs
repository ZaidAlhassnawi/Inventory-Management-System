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

namespace InventoryManagementSystem_DataAccessLayer.SupplierRepository
{
    public class AddSupplierRepostiory : IAddRepository<SupplierDTO>
    {
        private readonly string _connectionString;

        public AddSupplierRepostiory(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        
        public async Task<int> AddAsync(SupplierDTO obj)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_Suppliers_Insert", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SupplierName", obj.SupplierName);
                command.Parameters.AddWithValue("@ContactPerson", (object?)obj.ContactPerson ?? DBNull.Value);
                command.Parameters.AddWithValue("@Email", (object?)obj.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@PhoneNumber", (object?)obj.PhoneNumber ?? DBNull.Value);
                command.Parameters.AddWithValue("@Address", (object?)obj.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedDate", (object?)obj.CreatedDate ?? DBNull.Value);
              
                var outputIdParam = new SqlParameter("@NewSupplierID", SqlDbType.Int)
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
