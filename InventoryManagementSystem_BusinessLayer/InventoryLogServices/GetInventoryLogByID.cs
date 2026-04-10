using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.InventoryLogServices
{
    public class GetInventoryLogByID : IGetByIDService<InventoryLog>
    {
        private readonly IGetByIDRepository<InventoryLog> _GetByID;

        public GetInventoryLogByID(IGetByIDRepository<InventoryLog> getByID)
        {
            _GetByID = getByID;
        }
        public async Task<InventoryLog> FindAsync(int ID)
        {
            if (ID <= 0)
                return null;

            return await _GetByID.GetByIDAsync(ID);
        }
    }
}
