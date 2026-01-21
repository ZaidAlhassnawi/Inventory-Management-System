using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.CategoryServices
{
    public class GetAllCategoreiesService : IGetAllService<CategoryDTO>
    {
        private readonly IGetAllRepository<CategoryDTO> _GetAll;

        public GetAllCategoreiesService (IGetAllRepository<CategoryDTO> getAll)
        {
            _GetAll = getAll;
        }
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _GetAll.GetAllAsync();
        }
    }
}
