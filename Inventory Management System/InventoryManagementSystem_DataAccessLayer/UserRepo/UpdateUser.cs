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

namespace InventoryManagementSystem_DataAccessLayer.UserRepo
{

    public class UpdateUser : IUpdateRepository<User>
    {
        private readonly string _connectionString;

        public UpdateUser(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }
        public async Task<bool> UpdateAsync(User obj)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();

                using var command = new SqlCommand("SP_UpdatePerson", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UserID", obj.UserID);
                command.Parameters.AddWithValue("@FullName", obj.FullName);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@RollID", obj.RollID);

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
