using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_Model.Models
{
    public class InventoryLog
    {
        public int LogID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; } 
        public string? LogType { get; set; }     
        public int Quantity { get; set; }
        public int? PreviousStock { get; set; }
        public int? NewStock { get; set; }
        public string? Reason { get; set; }
        public int UserID { get; set; }
        public string? FullName { get; set; }   
        public DateTime LogDate { get; set; }

       
        public string QuantityDisplay => (LogType == "Stock In" ? "+" : "-") + Quantity;

      
        public string BadgeColor
        {
            get
            {
                if (LogType == "Stock In") return "#27AE60";
                if (LogType == "Stock Out") return "#EB5757";
                return "#3498DB"; 
            }
        }

        public string QuantityColor => BadgeColor;
    }
}
