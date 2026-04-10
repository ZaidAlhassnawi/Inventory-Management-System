using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.InventoryLogServices
{
    public class AddInventoryLogService : IAddService<InventoryLog>
    {
        private readonly IAddRepository<InventoryLog> _add;

        public AddInventoryLogService(IAddRepository<InventoryLog> add)
        {
            _add = add;
        }

        public async Task<bool> AddAsync(InventoryLog obj)
        {
           
            int newID = await _add.AddAsync(obj);
            return newID != -1;
        }
    }
}
