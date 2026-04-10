using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;


namespace InventoryManagementSystem_BusinessLayer.UserServices
{
    public class AddUserService : IAddService<User>
    {
        private readonly IAddRepository<User> _add;

        public AddUserService(IAddRepository<User> add)
        {
            _add = add;
        }

        public async Task<bool> AddAsync(User obj)
        {
            if (string.IsNullOrEmpty(obj.FullName) || string.IsNullOrEmpty(obj.Email) ||
                string.IsNullOrEmpty(obj.Password) || obj.RollID <= 0) 
                return false;

            int newID = await _add.AddAsync(obj);
            return newID != -1;
        }
    }
}
