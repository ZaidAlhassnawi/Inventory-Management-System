using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem_PresentaionLayer.Category.ViewModeCategory
{
    public partial class AddEditCategoryViewModel : ObservableValidator
    {
        private readonly IAddService<CategoryDTO> _addService;
        private readonly IUpdateService<CategoryDTO> _updateService;

        private CategoryDTO _CurrentCategory;
        public CategoryDTO CurrentCategory
        {
            get { return _CurrentCategory; }
            set
            {
                _CurrentCategory = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        [Required(ErrorMessage = "Category Name is required")]
        [NotifyDataErrorInfo]
        private string categoreName;

        public string Title => CurrentCategory.CategoryID > 0 ? "Update Category" : "Add New Category";
        public string btnTitle => CurrentCategory.CategoryID > 0 ? "Update Category" : "Add Category";


        public AddEditCategoryViewModel(IAddService<CategoryDTO> addService,IUpdateService<CategoryDTO> updateService)
        {
            _addService = addService;
            _updateService = updateService;

            CurrentCategory = new CategoryDTO();
           
        }

        public void LoadData(CategoryDTO category)
        {
            CurrentCategory = new CategoryDTO()
            {
                CategoryID = category.CategoryID,
                CategoreName = category.CategoreName,
                Description = category.Description,
                CreatedDate = category.CreatedDate
            };

            CategoreName = CurrentCategory.CategoreName;
        }

        [RelayCommand]
        private async Task Save(object parameter)
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                var message = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));
                MessageBox.Show($"الأخطاء الموجودة:\n{message}", "خطأ في الإدخال", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IsBusy = true;
            try
            {
              
                CurrentCategory.CategoreName = CategoreName;

                bool isSaved;

                if (CurrentCategory.CategoryID > 0)
                {
                    isSaved = await _updateService.UpdateAsync(CurrentCategory);
                }
                else
                {
                    CurrentCategory.CreatedDate = DateTime.Now;
                    isSaved = await _addService.AddAsync(CurrentCategory);
                }

                if (!isSaved)
                {
                    MessageBox.Show("Operation failed. An error occurred while saving to the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // 4. إغلاق النافذة
                if (parameter is Window currentWindow)
                {

                    currentWindow.DialogResult = true;
                    currentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while:\n{ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Cancel(object parameter)
        {
            if (parameter is Window currentWindow)
                currentWindow.Close();
        }
    }
}
