using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_DataAccessLayer.SupplierRepository
{
    public class GetAllSuppliersRepository : IGetAllRepository<SupplierDTO>
    {
        private readonly string _connectionString;

        public GetAllSuppliersRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }

        public async Task<List<SupplierDTO>> GetAllAsync()
        {
            var list = new List<SupplierDTO>();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                var result = await connection.QueryAsync<SupplierDTO>("SP_Suppliers_GetAll", commandType: CommandType.StoredProcedure);

                list = result.ToList();
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
