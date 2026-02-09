using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_PresentaionLayer.Category.ViewCategory;
using InventoryManagementSystem_PresentaionLayer.Products_W;
using InventoryManagementSystem_PresentaionLayer.ViewUser;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using _User = InventoryManagementSystem_Model.Models.User;
using Color = System.Windows.Media.Color;

namespace InventoryManagementSystem_PresentaionLayer.MainSideBar.ViewModelsSideBar
{
    public partial class SideBarViewModel: ObservableValidator
    {
        public _User CurrentUser
        {
            get { return Global.Global.CurrentUser; }
        }


        [RelayCommand]
        private void OpenProducts(object parameter)
        {
           
            if (parameter is not Window currentPage) return;

            var sideBarWindow = Window.GetWindow(currentPage) as Window1;

            if (sideBarWindow != null)
            {
                
                var app = (App)Application.Current;
                var signupPage = app._host.Services.GetRequiredService<Product_w1>();

                sideBarWindow.MainFrame.Navigate(signupPage);
            }
        }

        [RelayCommand]
        private void OpenCategories(object parameter)
        {
            if (parameter is not Window currentPage) return;
            var sideBarWindow = Window.GetWindow(currentPage) as Window1;
            if (sideBarWindow != null)
            {
                var app = (App)Application.Current;
                var signupPage = app._host.Services.GetRequiredService<CategoryWindow>();
                sideBarWindow.MainFrame.Navigate(signupPage);
            }
        }
    }
}
