using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.CategoryServices
{
    public class AddCategoryService : IAddService<CategoryDTO>
    {
        private readonly IAddRepository<CategoryDTO> _add;

        public AddCategoryService(IAddRepository<CategoryDTO> add)
        {
            _add = add;
        }

        public async Task<bool> AddAsync(CategoryDTO obj)
        {
            if (string.IsNullOrEmpty(obj.CategoreName))
                return false;

            int newID = await _add.AddAsync(obj);
            return newID != -1;
        }
    }
}
