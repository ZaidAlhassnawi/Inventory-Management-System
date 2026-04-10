using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.InventoryLogServices
{
    public class IsExistsInventoryLogService : IIsExistsService<InventoryLog>
    {
        private readonly IIsExistsRepository<InventoryLog> _isExists;

        public IsExistsInventoryLogService(IIsExistsRepository<InventoryLog> isExists)
        {
            _isExists = isExists;
        }

        public async Task<bool> IsExistsAsync(int ID)
        {

            return await _isExists.IsExistsAsync(ID);
        }
    }
}
