using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using InventoryManagementSystem_PresentaionLayer.Products_W.Products_w;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem_PresentaionLayer.Products_W.Products_V
{
    public partial class Product_w1ViewModel: ObservableValidator
    {





        [RelayCommand]
        private void OpenAddProductWindow(object parameter)
        {

            var app = (App)Application.Current;
            var signupPage = app._host.Services.GetRequiredService<AddEditProducts>();

            signupPage.ShowDialog();

        }
    }
}
