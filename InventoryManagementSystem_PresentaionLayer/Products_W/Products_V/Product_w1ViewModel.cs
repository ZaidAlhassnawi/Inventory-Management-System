using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using InventoryManagementSystem_PresentaionLayer.Category.ViewCategory;
using InventoryManagementSystem_PresentaionLayer.Category.ViewModeCategory;
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using InventoryManagementSystem_PresentaionLayer.Products_W.Products_w;
using InventoryManagementSystem_PresentaionLayer.Supplier.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace InventoryManagementSystem_PresentaionLayer.Products_W.Products_V
{
    public partial class Product_w1ViewModel: ObservableValidator
    {

        private readonly IGetAllService<ProductDTO> _ProductRepo;

        private ObservableCollection<ProductDTO> _ProductList;
        public ObservableCollection<ProductDTO> ProductList
        {
            get { return _ProductList; }
            set
            {
                _ProductList = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private ProductDTO selectedProduct;

        [ObservableProperty]
        private bool isBusy;

        public Product_w1ViewModel(IGetAllService<ProductDTO> ProductRepo)
        {
            _ProductRepo = ProductRepo;

            LoadProducts();
        }

        private async void LoadProducts()
        {
            IsBusy = true; 
            try
            {
                var productList = await _ProductRepo.GetAllAsync();
                ProductList = new ObservableCollection<ProductDTO>(productList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsBusy = false; 
            }
        }

        [RelayCommand] 
        private void OpenAddProductWindow(object parameter)
        {
            var mainWin = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            if (mainWin != null) mainWin.BackgroundOpasity.Visibility = Visibility.Visible;

            var app = (App)Application.Current;
            var addEditWindow = app._host.Services.GetRequiredService<AddEditProducts>();

            addEditWindow.Owner = mainWin;
            addEditWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var result = addEditWindow.ShowDialog();

            if (mainWin != null) mainWin.BackgroundOpasity.Visibility = Visibility.Collapsed;

            if (result == true)
            {
                if (addEditWindow.DataContext is AddProductViewModel vm)
                {
                    var newItem = vm.CurrentProduct;

                    ProductList.Add(newItem);
                }
            }

           
        }

        [RelayCommand]
        private void OpenEditProduct(ProductDTO productDTO)
        {
            var mainWin = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            if (mainWin != null) mainWin.BackgroundOpasity.Visibility = Visibility.Visible;

            var app = (App)Application.Current;
            var addEditWindow = app._host.Services.GetRequiredService<AddEditProducts>();

            addEditWindow.Owner = mainWin;
            addEditWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (addEditWindow.DataContext is AddProductViewModel vm)
            {
                vm.LoadDataProduct(productDTO);
            }

            var result = addEditWindow.ShowDialog();

            if (mainWin != null) mainWin.BackgroundOpasity.Visibility = Visibility.Collapsed;

            if (result == true)
            {
                LoadProducts();
            }


        }
    }
}
