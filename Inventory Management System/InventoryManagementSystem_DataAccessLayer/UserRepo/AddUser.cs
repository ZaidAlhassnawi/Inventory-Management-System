using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace InventoryManagementSystem_DataAccessLayer.UserRepo
{
    public class AddUser : IAddRepository<User>
    {
        private readonly string _connectionString;

        public AddUser(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }

        public async Task<int> AddAsync(User obj)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand("SP_AddNewUser", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FullName", obj.FullName);
                command.Parameters.AddWithValue("@Email", obj.Email);
                command.Parameters.AddWithValue("@PasswordHash", obj.Password);
                command.Parameters.AddWithValue("@RoleID", obj.RollID);

                var outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
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
