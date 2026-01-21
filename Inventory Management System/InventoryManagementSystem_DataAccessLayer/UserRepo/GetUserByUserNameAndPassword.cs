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
    public class GetUserByUserNameAndPassword : IGetUserByUserNameAndPassword
    {
        private readonly string _connectionString;

        public GetUserByUserNameAndPassword(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }
        
        public async Task<User> FindAsync(string Email)
        {
            User user = null;

            try
            {
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();

                using var command = new SqlCommand("SP_GetUserByUserNameAndPassword", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", Email);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user = new User();
                    user.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                    user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    user.Email = reader.GetString(reader.GetOrdinal("Email"));
                    user.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                    user.RoleName = reader.GetString(reader.GetOrdinal("RoleName"));
                    user.RollID = reader.GetInt32(reader.GetOrdinal("RoleID"));
                    user.Password = reader.GetString(reader.GetOrdinal("PasswordHash"));

                }
                else
                {
                    user = null;
                }



            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return user;
        }
    }
}
