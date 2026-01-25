using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.SupplierServices
{
    public class GetAllSuppliersService : IGetAllService<SupplierDTO>
    {
        private readonly IGetAllRepository<SupplierDTO> _GetAll;

        public GetAllSuppliersService(IGetAllRepository<SupplierDTO> getAll)
        {
            _GetAll = getAll;
        }
        public async Task<List<SupplierDTO>> GetAllAsync()
        {
            return await _GetAll.GetAllAsync();
        }
    }
}
