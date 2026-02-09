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
        public AddEditCategoryViewModel(IAddService<CategoryDTO> addService)
        {
            _addService = addService;

            CurrentCategory = new CategoryDTO();
        }


        [RelayCommand]
        private async Task Save(object parameter)
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                var message = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));

                MessageBox.Show($"الأخطاء الموجودة:\n{message}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            IsBusy = true;
            try
            {


                CurrentCategory.CategoreName = CategoreName;
                CurrentCategory.CreatedDate = DateTime.Now;

                bool isSaved = await _addService.AddAsync(CurrentCategory);

                if (!isSaved)
                {
                    MessageBox.Show("Category is not Added. An Error Occurre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Category saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                if (parameter is Window currentWindow)
                {
                    currentWindow.DialogResult = true;
                    currentWindow.Close();
                }

            }
            catch (Exception ex)
            {
                // معالجة الأخطاء
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
