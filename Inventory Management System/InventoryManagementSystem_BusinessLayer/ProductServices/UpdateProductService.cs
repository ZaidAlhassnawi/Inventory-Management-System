using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.ProductServices
{
    public class UpdateProductService: IUpdateService<ProductDTO>
    {
        private readonly IUpdateRepository<ProductDTO> _Update;

        public UpdateProductService(IUpdateRepository<ProductDTO> update)
        {
            _Update = update;
        }

        public async Task<bool> UpdateAsync(ProductDTO obj)
        {
            return await _Update.UpdateAsync(obj);
        }
    }
}
