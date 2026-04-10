using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem_PresentaionLayer.Supplier.ViewModel
{
    public partial class AddEditSupplierViewModel : ObservableValidator
    {
        private readonly IAddService<SupplierDTO> _AddSupplierService;
        private readonly IUpdateService<SupplierDTO> _UpdateSupplierService;

        private int _currentSupplierId = 0;

        [ObservableProperty]
        private string windowTitle = "Add New Supplier";

        [ObservableProperty]
        private string buttonText = "Add Supplier";

        [ObservableProperty]
        [Required(ErrorMessage = "Supplier name is required")]
        private string supplierName;

        [ObservableProperty]
        [Required(ErrorMessage = "Contact person is required")]
        private string contactPerson;

        [ObservableProperty]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        private string email;

        [ObservableProperty]
        [Phone(ErrorMessage = "Invalid phone number")]
        private string phoneNumber;

        [ObservableProperty]
        private string address;

        [ObservableProperty]
        private DateTime createdDate;
        public AddEditSupplierViewModel(IAddService<SupplierDTO> AddSupplierService,IUpdateService<SupplierDTO> UpdateSupplierService)
        {
            _AddSupplierService = AddSupplierService;
            _UpdateSupplierService = UpdateSupplierService;
        }

        public void LoadData(SupplierDTO supplier)
        {
            if (supplier == null) return;

            _currentSupplierId = supplier.SupplierID;
            SupplierName = supplier.SupplierName;
            ContactPerson = supplier.ContactPerson;
            Email = supplier.Email;
            PhoneNumber = supplier.PhoneNumber;
            Address = supplier.Address;
            CreatedDate = supplier.CreatedDate;

            WindowTitle = "Edit Supplier";
            ButtonText = "Update Supplier";
        }

        [RelayCommand]
        private async Task Save(Window window)
        {
            ValidateAllProperties();
            if (HasErrors) return;

            var supplierDto = new SupplierDTO
            {
                SupplierID = _currentSupplierId,
                SupplierName = SupplierName,
                ContactPerson = ContactPerson,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Address = Address,
                CreatedDate = _currentSupplierId == 0 ? DateTime.Now : CreatedDate

            };

            bool success;
            if (_currentSupplierId == 0)
                success = await _AddSupplierService.AddAsync(supplierDto);
            else
                success = await _UpdateSupplierService.UpdateAsync(supplierDto);

            if (success)
            {
                window.DialogResult = true;
                window.Close();
            }
            else
            {
                MessageBox.Show("Operation failed. Please try again.");
            }
        }

        [RelayCommand]
        private void Cancel(Window window)
        {
            window.DialogResult = false;
            window.Close();
        }
    }
}