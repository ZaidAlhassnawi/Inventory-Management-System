using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.UserServices
{
    public class GetAllUsersService : IGetAllService<User>
    {
        private readonly IGetAllRepository<User> _GetAll;

        public GetAllUsersService(IGetAllRepository<User> getAll)
        {
            _GetAll = getAll;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _GetAll.GetAllAsync();
        }
    }
}
