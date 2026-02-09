using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using InventoryManagementSystem_PresentaionLayer.Category.ViewCategory;
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using InventoryManagementSystem_PresentaionLayer.Products_W.Products_w;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace InventoryManagementSystem_PresentaionLayer.Category.ViewModeCategory
{
    public partial class CategoryViewModel : ObservableValidator
    {
        private readonly IGetAllService<CategoryDTO> _getAllService;

        private ObservableCollection<CategoryDTO> _CategoryList;
        public ObservableCollection<CategoryDTO> CategoryList
        {
            get { return _CategoryList; }
            set
            {
                _CategoryList = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private bool isBusy;

        public CategoryViewModel(IGetAllService<CategoryDTO> getAllService)
        {
            _getAllService = getAllService;

            LoadProducts();
        }

        private async void LoadProducts()
        {
            IsBusy = true;
            try
            {
                var categoryList = await _getAllService.GetAllAsync();
                CategoryList = new ObservableCollection<CategoryDTO>(categoryList);
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
        private void OpenAddEditCateogryWindow(object parameter)
        {
            var mainWin = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            if (mainWin != null) 
                mainWin.BackgroundOpasity.Visibility = Visibility.Visible;

            var app = (App)Application.Current;
            var addEditWindow = app._host.Services.GetRequiredService<AddEditCategoryWindow>();

            addEditWindow.Owner = mainWin;
            addEditWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var result = addEditWindow.ShowDialog();

            if (mainWin != null) mainWin.BackgroundOpasity.Visibility = Visibility.Collapsed;

            if (result == true)
            {
                if (addEditWindow.DataContext is AddEditCategoryViewModel vm)
                {
                    var newItem = vm.CurrentCategory;

                    CategoryList.Add(newItem);
                }
            }
        }

    }
}
