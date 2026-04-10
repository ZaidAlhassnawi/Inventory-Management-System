using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;

namespace InventoryManagementSystem_PresentaionLayer.Inventory_Logs.ViewInventoryLogs
{
    public partial class StockAdjustmentViewModel : ObservableValidator
    {
        private readonly IGetAllService<ProductDTO> _allProduct;
        private readonly IAddService<InventoryLog> _addService;
        private readonly IUpdateService<InventoryLog> _updateService;
        private readonly IIsExistsService<InventoryLog> _isExistsService;
        private readonly IGetByIDService<ProductDTO> _getByIDService;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Quantity cannot be empty")]
        private int? _quantityChange;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Please provide a reason")]
        private string _reason;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Please select a product")]
        private ProductDTO _selectedProduct;

        [ObservableProperty]
        private ObservableCollection<ProductDTO> _productList = new();

        public StockAdjustmentViewModel(
            IGetAllService<ProductDTO> getAllService,
            IAddService<InventoryLog> addService,
            IUpdateService<InventoryLog> updateService,
            IIsExistsService<InventoryLog> isExistsService,
             IGetByIDService<ProductDTO> getByIDService)
        {
            _allProduct = getAllService;
            _addService = addService;
            _updateService = updateService;
            _isExistsService = isExistsService;
            _getByIDService = getByIDService;

            _ = LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var products = await _allProduct.GetAllAsync();
                ProductList = new ObservableCollection<ProductDTO>(products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task AdjustStockAsync(Window window)
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                MessageBox.Show("Please fix the errors before submitting.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedProduct != null && QuantityChange != null && QuantityChange != 0)
            {
                try
                {
                    var Product = await _getByIDService.FindAsync(SelectedProduct.ProductID);

                    string logType = QuantityChange.Value > 0 ? "Stock In" : "Stock Out";

                    int absoluteQuantity = Math.Abs(QuantityChange.Value);

                    int pStock = (Product.Stock ?? 0) + QuantityChange.Value;
                    
                    var inventoryLog = new InventoryLog
                    {
                        ProductID = SelectedProduct.ProductID, 
                        LogType = logType,
                        Quantity = absoluteQuantity,
                        PreviousStock = Product.Stock,
                        NewStock = pStock,
                        Reason = Reason,
                        LogDate = DateTime.Now,

                        UserID = Global.Global.CurrentUser.UserID,
                
                    };

                    bool isExists = await _isExistsService.IsExistsAsync(SelectedProduct.ProductID);

                    if (isExists)
                    {
                        await _updateService.UpdateAsync(inventoryLog);
                        MessageBox.Show($"Stock for '{SelectedProduct.ProductName}' updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        await _addService.AddAsync(inventoryLog);
                        MessageBox.Show($"Stock for '{SelectedProduct.ProductName}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                    window.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}