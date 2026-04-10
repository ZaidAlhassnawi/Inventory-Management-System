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
using System.Collections.Specialized;
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
        private readonly IDeleteService<CategoryDTO> _deleteService;

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

        [ObservableProperty]
        public int count;
        [ObservableProperty]
        public string countString;
        public CategoryViewModel(IGetAllService<CategoryDTO> getAllService,IDeleteService<CategoryDTO> deleteService)
        {
            _getAllService = getAllService;
            _deleteService = deleteService;

            _=LoadProducts();
        }

   
        private async Task LoadProducts()
        {
            IsBusy = true;
            try
            {
                var categoryList = await _getAllService.GetAllAsync();
                CategoryList = new ObservableCollection<CategoryDTO>(categoryList);

                Count = CategoryList?.Count ?? 0;
                CountString = $"Categories ({Count})";
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
                _ = LoadProducts();
            }
        }

        [RelayCommand]
        private void OpenEditCategoryWindow(CategoryDTO SelectedCategory)
        {
            var mainWin = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            if (mainWin != null) mainWin.BackgroundOpasity.Visibility = Visibility.Visible;

            var app = (App)Application.Current;
            var addEditWindow = app._host.Services.GetRequiredService<AddEditCategoryWindow>();

            addEditWindow.Owner = mainWin;
            addEditWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var vm = addEditWindow.DataContext as AddEditCategoryViewModel;
            if (vm != null && SelectedCategory != null)
            {
                vm.LoadData(SelectedCategory);
            }

            var result = addEditWindow.ShowDialog();

            if (mainWin != null) mainWin.BackgroundOpasity.Visibility = Visibility.Collapsed;

            if (result == true && vm != null)
            {
                _ = LoadProducts();
            }
        }

        [RelayCommand]
        private async Task Delete(CategoryDTO SelectedCategory)
        {
            try
            {
                var result = MessageBox.Show($"Are You Sure you want to delete{SelectedCategory.CategoreName}", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            
                if(result == MessageBoxResult.Yes)
                {
                    bool isDeleted = await _deleteService.DeleteAsync(SelectedCategory.CategoryID);
                    if(isDeleted)
                    {
                        MessageBox.Show("Category is deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        CategoryList.Remove(SelectedCategory);

                        Count = CategoryList?.Count ?? 0;
                        CountString = $"Categories ({Count})";
                    }
                    else
                    {
                        MessageBox.Show("Category is not deleted, Operation failed. An error occurred.", "Success", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
            }
            catch
            {

            }
        }
    }
}
