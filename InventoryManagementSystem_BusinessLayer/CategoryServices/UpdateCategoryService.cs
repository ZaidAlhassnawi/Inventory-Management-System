using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.CategoryServices
{
    public class UpdateCategoryService: IUpdateService<CategoryDTO>
    {
        private readonly IUpdateRepository<CategoryDTO> _Update;

        public UpdateCategoryService(IUpdateRepository<CategoryDTO> update)
        {
            _Update = update;
        }

        public async Task<bool> UpdateAsync(CategoryDTO obj)
        {
            return await _Update.UpdateAsync(obj);
        }
    }
}
