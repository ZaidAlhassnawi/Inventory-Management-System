using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models; // تأكد أن SupplierDTO موجود هنا
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using InventoryManagementSystem_PresentaionLayer.Supplier.SupplierView;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem_PresentaionLayer.Supplier.ViewModel
{
    public partial class SupplierViewModel : ObservableValidator
    {
        private readonly IGetAllService<SupplierDTO> _getAllService;
        private readonly IDeleteService<SupplierDTO> _deleteService;

        private List<SupplierDTO> _allSuppliers = new();

        [ObservableProperty]
        private ObservableCollection<SupplierDTO> filteredSuppliers;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private string countString;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterSuppliers();
            }
        }

        public SupplierViewModel(IGetAllService<SupplierDTO> getAllService, IDeleteService<SupplierDTO> deleteService)
        {
            _getAllService = getAllService;
            _deleteService = deleteService;

            _ = LoadSuppliers();
        }

        private async Task LoadSuppliers()
        {
            IsBusy = true;
            try
            {
                var list = await _getAllService.GetAllAsync();
                _allSuppliers = list.ToList();
                FilterSuppliers(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FilterSuppliers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredSuppliers = new ObservableCollection<SupplierDTO>(_allSuppliers);
            }
            else
            {
                var lowerSearch = SearchText.ToLower();
                var filtered = _allSuppliers.Where(s =>
                    (s.SupplierName?.ToLower().Contains(lowerSearch) ?? false) ||
                    (s.ContactPerson?.ToLower().Contains(lowerSearch) ?? false) ||
                    (s.Email?.ToLower().Contains(lowerSearch) ?? false)
                ).ToList();

                FilteredSuppliers = new ObservableCollection<SupplierDTO>(filtered);
            }

            Count = FilteredSuppliers.Count;
            CountString = $"Suppliers ({Count})";
        }

        [RelayCommand]
        private void OpenAddSupplierWindow()
        {
            ToggleMainOpacity(true);

            var app = (App)Application.Current;
            var addEditWindow = app._host.Services.GetRequiredService<AddEditSupplierWindow>();

            addEditWindow.Owner = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            addEditWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var result = addEditWindow.ShowDialog();
            ToggleMainOpacity(false);

            if (result == true) _ = LoadSuppliers();
        }

        [RelayCommand]
        private void OpenEditSupplierWindow(SupplierDTO selectedSupplier)
        {
            if (selectedSupplier == null) return;

            ToggleMainOpacity(true);

            var app = (App)Application.Current;
            var addEditWindow = app._host.Services.GetRequiredService<AddEditSupplierWindow>();

            addEditWindow.Owner = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            addEditWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (addEditWindow.DataContext is AddEditSupplierViewModel vm)
            {
                vm.LoadData(selectedSupplier);
            }

            var result = addEditWindow.ShowDialog();
            ToggleMainOpacity(false);

            if (result == true) _ = LoadSuppliers();
        }

        [RelayCommand]
        private async Task Delete(SupplierDTO selectedSupplier)
        {
            if (selectedSupplier == null) return;

            var result = MessageBox.Show($"Are you sure you want to delete {selectedSupplier.SupplierName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                bool isDeleted = await _deleteService.DeleteAsync(selectedSupplier.SupplierID);
                if (isDeleted)
                {
                    _allSuppliers.Remove(selectedSupplier);
                    FilterSuppliers();
                    MessageBox.Show("Supplier deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete supplier.");
                }
            }
        }

        private void ToggleMainOpacity(bool isVisible)
        {
            var mainWin = Application.Current.Windows.OfType<Window1>().FirstOrDefault();
            if (mainWin != null)
            {
                mainWin.BackgroundOpasity.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}