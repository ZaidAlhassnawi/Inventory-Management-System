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

namespace InventoryManagementSystem_DataAccessLayer.UserRepo
{
    public class DeleteUser : IDeleteRepository<User>
    {
        private readonly string _connectionString;

        public DeleteUser(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }

        public async Task<bool> DelteAsync(int ID)
        {
            int rowsAffected = 0;

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_DeleteUser", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", ID);
                rowsAffected = await command.ExecuteNonQueryAsync();


            }
            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return (rowsAffected > 0);
        }
    }
}
