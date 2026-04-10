using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InventoryManagementSystem_PresentaionLayer.Products_W.Products_V
{
    public partial class AddProductViewModel : ObservableValidator
    {
        private readonly IAddService<ProductDTO> _ProductRepo;
        private readonly IUpdateService<ProductDTO> _UpdateRepo; 
        private readonly IGetAllService<CategoryDTO> _CategoreRepo;
        private readonly IGetAllService<SupplierDTO> _SupplierRepo;

        [ObservableProperty]
        private ProductDTO currentProduct;

        [ObservableProperty]
        private ObservableCollection<CategoryDTO> categoreyList;

        [ObservableProperty]
        private ObservableCollection<SupplierDTO> supplierList;

        [ObservableProperty]
        private bool isBusy;

        // متغير لتحديد هل نحن في وضع التعديل أم الإضافة
        [ObservableProperty]
        private string windowTitle = "Add New Product";

        #region Validation Properties
        [ObservableProperty]
        [Required(ErrorMessage = "Stock is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Stock must be a whole number")]
        private string stock;

        [ObservableProperty]
        [Required(ErrorMessage = "Cost price is required")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Only numbers and decimal point allowed")]
        private string costPrice;

        [ObservableProperty]
        [Required(ErrorMessage = "Selling price is required")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Selling Price must be a positive value")]
        private string sellingPrice;

        [ObservableProperty]
        [Required(ErrorMessage = "SKU is required")]
        [MinLength(5, ErrorMessage = "SKU must be at least 5 characters")]
        private string sKU;

        [ObservableProperty]
        [Required(ErrorMessage = "Product Name is required")]
        private string productName;

        [ObservableProperty]
        private ImageSource? selectedImagePreview;

        [ObservableProperty]
        private string imagePathText;
        #endregion

        public AddProductViewModel(IAddService<ProductDTO> ProductRepo,
                                 IGetAllService<CategoryDTO> CategoreRepo,
                                 IGetAllService<SupplierDTO> SupplierRepo,
                                 IUpdateService<ProductDTO> UpdateRepo)
        {
            _ProductRepo = ProductRepo;
            _CategoreRepo = CategoreRepo;
            _SupplierRepo = SupplierRepo;
            _UpdateRepo = UpdateRepo;

            CurrentProduct = new ProductDTO();
            LoadDataAsync();
        }

        public void LoadDataProduct(ProductDTO productDTO)
        {
            if (productDTO == null) return;

            CurrentProduct = new ProductDTO();

            WindowTitle = "Edit Product: " + productDTO.ProductName;

            ProductName = productDTO.ProductName;

            SKU = productDTO.SKU;
            Stock = productDTO.Stock.ToString();
            CostPrice = productDTO.CostPrice.ToString();
            SellingPrice = productDTO.SellingPrice.ToString();
            ImagePathText = productDTO.ImageURL;

            if (!string.IsNullOrEmpty(productDTO.ImageURL))
            {
                try
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, productDTO.ImageURL);
                    if (File.Exists(fullPath))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(fullPath);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        SelectedImagePreview = bitmap;
                    }
                }
                catch { /* ignored */ }
            }

            CurrentProduct = productDTO;
        }

        private async void LoadDataAsync()
        {
            var categoreList = await _CategoreRepo.GetAllAsync();
            CategoreyList = new ObservableCollection<CategoryDTO>(categoreList);

            var supplierList = await _SupplierRepo.GetAllAsync();
            SupplierList = new ObservableCollection<SupplierDTO>(supplierList);
        }

        [RelayCommand]
        private async Task Save(object parameter)
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                var message = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));
                MessageBox.Show($"Errors:\n{message}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IsBusy = true;
            try
            {
                CurrentProduct.ProductName = ProductName;
                CurrentProduct.SKU = SKU;
                CurrentProduct.Stock = int.Parse(Stock);
                CurrentProduct.CostPrice = decimal.Parse(CostPrice);
                CurrentProduct.SellingPrice = decimal.Parse(SellingPrice);
                CurrentProduct.ImageURL = ImagePathText;

                bool isSuccess;
                if (CurrentProduct.ProductID == 0) 
                {
                    isSuccess = await _ProductRepo.AddAsync(CurrentProduct);
                }
                else 
                {
                     isSuccess = await _UpdateRepo.UpdateAsync(CurrentProduct);
                }

                if (isSuccess)
                {
                    MessageBox.Show("Product saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (parameter is Window currentWindow)
                    {
                        currentWindow.DialogResult = true;
                        currentWindow.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private void SelectImage()
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Select Product Image",
                Filter = "Images|*.jpg;*.jpeg;*.png"
            };

            if (op.ShowDialog() == true)
            {
                ProcessSelectedImage(op.FileName);
            }
        }

        private void ProcessSelectedImage(string sourceFilePath)
        {
            try
            {
                string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductImages");
                if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);

                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFilePath);
                string destinationPath = Path.Combine(imagesFolder, newFileName);

                File.Copy(sourceFilePath, destinationPath, true);

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(destinationPath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                SelectedImagePreview = bitmap;
                ImagePathText = Path.Combine("ProductImages", newFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        [RelayCommand]
        private void Cancel(object parameter)
        {
            if (parameter is Window currentWindow) currentWindow.Close();
        }
    }
}