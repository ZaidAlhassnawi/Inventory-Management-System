using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace InventoryManagementSystem_PresentaionLayer.Products_W.Products_V
{
    public partial class AddProductViewModel:ObservableValidator
    {
        private readonly IAddService<ProductDTO> _ProductRepo;
        private readonly IGetAllService<CategoryDTO> _CategoreRepo;
        private readonly IGetAllService<SupplierDTO> _SupplierRepo;


        private ProductDTO _currentProduct;

        public ProductDTO CurrentProduct
        {
            get { return _currentProduct; }    
            set
            {
                _currentProduct = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CategoryDTO> _categoreyList;
        public ObservableCollection<CategoryDTO> CategoreyList 
        { get { return _categoreyList; }
            set
            {
                _categoreyList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SupplierDTO> _supplierList;
        public ObservableCollection<SupplierDTO> SupplierList
        {
            get { return _supplierList; }
            set
            {
                _supplierList = value;
                OnPropertyChanged();
            }
        }

        #region Property
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive value")]
        [NotifyDataErrorInfo]
        private string stock;

        [Required(ErrorMessage = "Cost price is required")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Only numbers and decimal point allowed")]
        [ObservableProperty]
        private string costPrice;
       

        [ObservableProperty]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Selling Price must be a positive value")]
        [Required(ErrorMessage = "Selling price is required")]
        private string sellingPrice;

        [ObservableProperty]
        [Required(ErrorMessage = "SKU is required")]
        [MinLength(5, ErrorMessage = "SKU must be at least 5 characters")]
        [NotifyDataErrorInfo]
        private string sKU;


        [ObservableProperty]
        [Required(ErrorMessage = "Product Name is required")]
        [NotifyDataErrorInfo]
        private string productName;


        [ObservableProperty]
        private ImageSource? _selectedImagePreview;
        
        [ObservableProperty]
        private string _imagePathText;
        #endregion


        public AddProductViewModel(IAddService<ProductDTO> ProductRepo, IGetAllService<CategoryDTO> CategoreRepo,
            IGetAllService<SupplierDTO> SupplierRepo)
        {
            _ProductRepo = ProductRepo;

            _CategoreRepo = CategoreRepo;

            _SupplierRepo = SupplierRepo;

            CurrentProduct = new ProductDTO();

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            var categoreList = await _CategoreRepo.GetAllAsync();

            CategoreyList = new ObservableCollection<CategoryDTO>(categoreList);

            var supplierList = await _SupplierRepo.GetAllAsync();

            SupplierList = new ObservableCollection<SupplierDTO>(supplierList);
        }

        #region Command
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
                

                CurrentProduct.ProductName = ProductName;
                CurrentProduct.SKU = SKU;
                CurrentProduct.Stock = int.Parse(Stock);
                CurrentProduct.CostPrice = decimal.Parse(CostPrice);
                CurrentProduct.SellingPrice = decimal.Parse(SellingPrice);

                if (!string.IsNullOrEmpty(ImagePathText))
                {
                     CurrentProduct.ImageURL = ImagePathText;
                }
                else
                {
                    CurrentProduct.ImageURL = null;
                }
                    
                //CurrentProduct.Description =

                bool isSaved = await _ProductRepo.AddAsync(CurrentProduct);

                if (!isSaved)
                {
                    MessageBox.Show("Product is not Added. An Error Occurre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Product saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                if (parameter is Window currentWindow)
                    currentWindow.Close();

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

        #endregion

        #region Image
        [RelayCommand]
        private void SelectImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select Product Image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                ProcessSelectedImage(op.FileName);
            }
        }

        private void ProcessSelectedImage(string sourceFilePath)
        {
            try
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolder = Path.Combine(projectPath, "ProductImages"); 

                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string fileExtension = Path.GetExtension(sourceFilePath);
                string newFileName = Guid.NewGuid().ToString() + fileExtension;
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
                MessageBox.Show($"Error uploading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Validation of Price
        partial void OnCostPriceChanged(string value)
        {
            ValidateProperty(value, nameof(CostPrice));
        }

        partial void OnSellingPriceChanged(string value)
        {
            ValidateProperty(value, nameof(SellingPrice));
        }
        #endregion
    }
}
