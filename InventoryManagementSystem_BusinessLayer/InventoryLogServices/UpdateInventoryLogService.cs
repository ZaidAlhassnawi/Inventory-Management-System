using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.InventoryLogServices
{
    public class UpdateInventoryLogService: IUpdateService<InventoryLog>
    {
        private readonly IUpdateRepository<InventoryLog> _Update;

        public UpdateInventoryLogService(IUpdateRepository<InventoryLog> update)
        {
            _Update = update;
        }

        public async Task<bool> UpdateAsync(InventoryLog obj)
        {
            return await _Update.UpdateAsync(obj);
        }
    }
}
