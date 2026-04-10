using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryManagementSystem_PresentaionLayer.Inventory_Logs.ViewInventoryLogs
{
    /// <summary>
    /// Interaction logic for StockAdjustmentView.xaml
    /// </summary>
    public partial class StockAdjustmentView : Window
    {
        public StockAdjustmentView(StockAdjustmentViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
