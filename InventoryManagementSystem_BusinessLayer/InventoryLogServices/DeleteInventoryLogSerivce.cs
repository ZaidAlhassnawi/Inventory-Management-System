using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.InventoryLogServices
{
    public class DeleteInventoryLogSerivce : IDeleteService<InventoryLog>
    {
        private readonly IDeleteRepository<InventoryLog> _Delete;

        public DeleteInventoryLogSerivce(IDeleteRepository<InventoryLog> delete)
        {
            _Delete = delete;
        }


        public async Task<bool> DeleteAsync(int ID)
        {
            if (ID <= 0)
                return false;

            return await _Delete.DelteAsync(ID);
        }
    }
}
