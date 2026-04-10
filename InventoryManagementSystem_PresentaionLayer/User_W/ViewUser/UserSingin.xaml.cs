using InventoryManagementSystem_PresentaionLayer.UserWindwo.ViewModeles;
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

namespace InventoryManagementSystem_PresentaionLayer.ViewUser
{
    /// <summary>
    /// Interaction logic for UserSingin.xaml
    /// </summary>
    public partial class UserSingin : Page
    {
        public UserSingin(LoginViewModel vm)
        {
            InitializeComponent();

            this.DataContext = vm;
            
        }

    }
}
