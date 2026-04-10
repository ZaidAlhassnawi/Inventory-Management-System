using InventoryManagementSystem_Model.Models;
using InventoryManagementSystem_PresentaionLayer.Supplier.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagementSystem_PresentaionLayer.Supplier.SupplierView
{
    /// <summary>
    /// Interaction logic for SupplierWindow.xaml
    /// </summary>
    public partial class SupplierWindow : Page
    {
        public SupplierWindow(SupplierViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
