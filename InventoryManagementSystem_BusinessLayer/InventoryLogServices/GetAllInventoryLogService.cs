using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.InventoryLogServices
{
    public class GetAllInventoryLogService: IGetAllService<InventoryLog>
    {
        private readonly IGetAllRepository<InventoryLog> _GetAll;

        public GetAllInventoryLogService (IGetAllRepository<InventoryLog> getAll)
        {
            _GetAll = getAll;
        }
        public async Task<List<InventoryLog>> GetAllAsync()
        {
            return await _GetAll.GetAllAsync();
        }
    }
}
