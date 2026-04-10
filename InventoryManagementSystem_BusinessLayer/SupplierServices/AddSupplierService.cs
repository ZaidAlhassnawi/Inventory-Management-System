using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.SupplierServices
{
    public class AddSupplierService : IAddService<SupplierDTO>
    {
        private readonly IAddRepository<SupplierDTO> _add;

        public AddSupplierService(IAddRepository<SupplierDTO> add)
        {
            _add = add;
        }

        public async Task<bool> AddAsync(SupplierDTO obj)
        {
            if (string.IsNullOrEmpty(obj.SupplierName))
                return false;

            int newID = await _add.AddAsync(obj);
            return newID != -1;
        }
    }
}
