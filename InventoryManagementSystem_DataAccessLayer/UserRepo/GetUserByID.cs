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

    public class GetUserByID : IGetByIDRepository<User>
    {
        private readonly string _connectionString;

        public GetUserByID(IOptions<DataAccessSettings> options)
        {
            _connectionString = options.Value.DBConnectionString;
        }
        public async Task<User> GetByIDAsync(int ID)
        {
            User user = null;

            try
            {
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();

                using var command = new SqlCommand("SP_GetUserByID", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", ID);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                    user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    user.Email = reader.GetString(reader.GetOrdinal("Email"));
                    user.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                    user.RoleName = reader.GetString(reader.GetOrdinal("RoleName"));
                    user.RollID = reader.GetInt32(reader.GetOrdinal("RoleID"));

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
