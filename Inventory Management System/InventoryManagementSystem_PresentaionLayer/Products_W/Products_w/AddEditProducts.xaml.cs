using InventoryManagementSystem_PresentaionLayer.Products_W.Products_V;
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

namespace InventoryManagementSystem_PresentaionLayer.Products_W.Products_w
{
    /// <summary>
    /// Interaction logic for AddEditProducts.xaml
    /// </summary>
    public partial class AddEditProducts : Window
    {
        public AddEditProducts(AddProductViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
