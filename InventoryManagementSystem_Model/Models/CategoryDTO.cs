using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_Model.Models
{
    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string? CategoreName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductCount { get; set; }
        
    }
}
