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

namespace InventoryManagementSystem_PresentaionLayer.User.ViewUser
{
    /// <summary>
    /// Interaction logic for UserSingnUp.xaml
    /// </summary>
    public partial class UserSingnUp : Page
    {
        private AddUserViewModel _viewModel;
        public UserSingnUp(AddUserViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            this.DataContext = _viewModel;
        }

        // حدث تغيير كلمة المرور
        private void TxtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox box)
            {
                // ننقل الباسورد يدوياً للفيوموديل
                _viewModel.Password = box.Password;
            }
        }

        // حدث تغيير تأكيد كلمة المرور
        private void TxtConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox box)
            {
                _viewModel.ConfirmPassword = box.Password;
            }
        }
    }
}
