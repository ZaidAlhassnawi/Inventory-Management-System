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

namespace InventoryManagementSystem_DataAccessLayer.SupplierRepository
{
    public class UpdateSupplierRepository: IUpdateRepository<SupplierDTO>
    {
        private readonly string _connectionString;

        public UpdateSupplierRepository(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        public async Task<bool> UpdateAsync(SupplierDTO obj)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("sp_Suppliers_Update", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SupplierID", obj.SupplierID);
                command.Parameters.AddWithValue("@SupplierName", obj.SupplierName);
                command.Parameters.AddWithValue("@ContactPerson", (object?)obj.ContactPerson ?? DBNull.Value);
                command.Parameters.AddWithValue("@Email", (object?)obj.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@PhoneNumber", (object?)obj.PhoneNumber ?? DBNull.Value);
                command.Parameters.AddWithValue("@Address", (object?)obj.Address ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedDate", (object?)obj.CreatedDate ?? DBNull.Value);

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
