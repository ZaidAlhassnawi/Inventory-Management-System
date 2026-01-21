using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_DataAccessLayer.ProductRepository
{
    public class GetProductByID : IGetByIDRepository<ProductDTO>
    {
        private readonly string _connectionString;

        public GetProductByID (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConnectionString");
        }
        public async Task<ProductDTO> GetByIDAsync(int ID)
        {
            ProductDTO product = new ProductDTO();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                product = await connection.QueryFirstOrDefaultAsync<ProductDTO>("sp_Products_GetById", new { Id = ID },
                    commandType: CommandType.StoredProcedure);


            }
            catch (SqlException ex)
            {
            }
            catch (Exception ex)
            {
            }

            return product;
        }
    }
}
