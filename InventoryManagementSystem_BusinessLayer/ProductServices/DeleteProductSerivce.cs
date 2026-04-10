using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_BusinessLayer.ProductServices
{
    public class DeleteProductSerivce : IDeleteService<ProductDTO>
    {
        private readonly IDeleteRepository<ProductDTO> _Delete;

        public DeleteProductSerivce(IDeleteRepository<ProductDTO> delete)
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
