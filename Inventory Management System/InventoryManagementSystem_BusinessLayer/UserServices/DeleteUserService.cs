using InventoryManagementSystem_Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.UserServices
{
    public class DeleteUserService : IDeleteService
    {
        private readonly IDeleteRepository<int> _Delete;

        public DeleteUserService(IDeleteRepository<int> delete)
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
