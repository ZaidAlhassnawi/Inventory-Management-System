using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.UserServices
{
    public class UpdateUserService : IUpdateService<User>
    {
        private readonly IUpdateRepository<User> _Update;

        public UpdateUserService(IUpdateRepository<User> update)
        {
            _Update = update;
        }

        public async Task<bool> UpdateAsync(User obj)
        {
            if (string.IsNullOrEmpty(obj.FullName) || string.IsNullOrEmpty(obj.Email) ||
                string.IsNullOrEmpty(obj.Password) || obj.RollID <= 0)
                return false;

            return await _Update.UpdateAsync(obj);
        }
    }
}
