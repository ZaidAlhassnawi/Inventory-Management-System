using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_Model.Models
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public int? Stock { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string? ImageURL { get; set; }
        public string? Description { get; set; }

        public string? CategoryName { get; set; }

        public string? SupplierName { get; set; }


    }
}
