using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.ProductServices
{
    public class AddProductService : IAddService<ProductDTO>
    {
        private readonly IAddRepository<ProductDTO> _add;

        public AddProductService(IAddRepository<ProductDTO> add)
        {
            _add = add;
        }

        public async Task<bool> AddAsync(ProductDTO obj)
        {
            if (string.IsNullOrEmpty(obj.ProductName) || string.IsNullOrEmpty(obj.SKU))
                return false;

            int newID = await _add.AddAsync(obj);
            return newID != -1;
        }
    }
}
