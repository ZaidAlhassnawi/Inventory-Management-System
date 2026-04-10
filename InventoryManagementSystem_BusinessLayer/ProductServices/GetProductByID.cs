using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.ProductServices
{
    public class GetProductByID : IGetByIDService<ProductDTO>
    {
        private readonly IGetByIDRepository<ProductDTO> _GetByID;

        public GetProductByID(IGetByIDRepository<ProductDTO> getByID)
        {
            _GetByID = getByID;
        }
        public async Task<ProductDTO> FindAsync(int ID)
        {
            if (ID <= 0)
                return null;

            return await _GetByID.GetByIDAsync(ID);
        }
    }
}
