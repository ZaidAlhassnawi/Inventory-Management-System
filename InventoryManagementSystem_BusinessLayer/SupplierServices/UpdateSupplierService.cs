using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.SupplierServices
{
    public class UpdateSupplierService: IUpdateService<SupplierDTO>
    {
        private readonly IUpdateRepository<SupplierDTO> _Update;

        public UpdateSupplierService(IUpdateRepository<SupplierDTO> update)
        {
            _Update = update;
        }

        public async Task<bool> UpdateAsync(SupplierDTO obj)
        {
            return await _Update.UpdateAsync(obj);
        }
    }
}
