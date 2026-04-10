using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.ProductServices
{
    public class GetAllProductsService: IGetAllService<ProductDTO>
    {
        private readonly IGetAllRepository<ProductDTO> _GetAll;

        public GetAllProductsService (IGetAllRepository<ProductDTO> getAll)
        {
            _GetAll = getAll;
        }
        public async Task<List<ProductDTO>> GetAllAsync()
        {
            return await _GetAll.GetAllAsync();
        }
    }
}
