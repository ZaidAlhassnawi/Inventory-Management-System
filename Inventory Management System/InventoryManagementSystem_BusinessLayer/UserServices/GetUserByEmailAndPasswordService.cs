using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.UserServices
{
    public class GetUserByEmailAndPasswordService :IGetByEmailAndPasswordService
    {
        private readonly IGetUserByUserNameAndPassword _Find;

        public GetUserByEmailAndPasswordService(IGetUserByUserNameAndPassword Find)
        {
            _Find = Find;
        }

        public async Task<User> FindAsync(string Email)
        {
            if (string.IsNullOrEmpty(Email))
                return null;

            return await _Find.FindAsync(Email);
        }
    }
}
