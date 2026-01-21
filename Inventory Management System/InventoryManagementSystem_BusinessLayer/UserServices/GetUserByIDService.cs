using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.UserServices
{
    public class GetUserByIDService : IGetByIDService<User>
    {
        private readonly IGetByIDRepository<User> _GetByID;

        public GetUserByIDService(IGetByIDRepository<User> getByID)
        {
            _GetByID = getByID;
        }
        public async Task<User> FindAsync(int ID)
        {
            if (ID <= 0)
                return null;

            return await _GetByID.GetByIDAsync(ID);
        }

    }
}
