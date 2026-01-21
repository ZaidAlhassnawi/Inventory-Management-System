using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_DataAccessLayer.UserRepo
{
    public class GetAllUser : IGetAllRepository<User>
    {
        private readonly string _connectionString;

        public GetAllUser(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }
        public async Task<List<User>> GetAllAsync()
        {
            var list = new List<User>();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();

                using var command = new SqlCommand("SP_GetAllUsers", connection);

                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new User
                        {
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                            RoleName = reader.GetString(reader.GetOrdinal("RoleName"))

                        });
                    }
                }

            }
            catch (SqlException ex)
            {

            }
            catch (Exception ex)
            {
                
            }
            return list;
        }
    }
}
