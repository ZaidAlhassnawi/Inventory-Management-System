using CommunityToolkit.Mvvm.ComponentModel;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem_PresentaionLayer.Products_W.Products_V
{
    public partial class AddProductViewModel:ObservableValidator
    {
        private readonly IAddService<ProductDTO> _ProductRepo;
        private readonly IGetAllService<CategoryDTO> _CategoreRepo;


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

        public AddProductViewModel(IAddService<ProductDTO> ProductRepo, IGetAllService<CategoryDTO> CategoreRepo)
        {
            _ProductRepo = ProductRepo;

            _CategoreRepo = CategoreRepo;

            CurrentProduct = new ProductDTO();

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            var categoreList = await _CategoreRepo.GetAllAsync();

            CategoreyList = new ObservableCollection<CategoryDTO>(categoreList);
            
        }
    }
}
