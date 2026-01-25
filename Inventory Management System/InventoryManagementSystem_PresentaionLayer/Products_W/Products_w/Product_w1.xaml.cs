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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagementSystem_PresentaionLayer.Products_W
{
    /// <summary>
    /// Interaction logic for Product_w1.xaml
    /// </summary>
    public partial class Product_w1 : Page
    {
        public Product_w1(Product_w1ViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
