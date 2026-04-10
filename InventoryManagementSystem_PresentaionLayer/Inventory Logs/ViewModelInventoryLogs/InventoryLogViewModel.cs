using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using Microsoft.Extensions.DependencyInjection; // لتفعيل DI
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem_PresentaionLayer.Inventory_Logs.ViewInventoryLogs
{
    public partial class InventoryLogViewModel : ObservableValidator
    {
        private readonly IGetAllService<InventoryLog> _getAllService;

        private List<InventoryLog> _allLogs = new();

        [ObservableProperty]
        private ObservableCollection<InventoryLog> filteredLogs;

        [ObservableProperty]
        private bool isBusy;

        // --- إحصائيات ---
        [ObservableProperty] private int totalLogsCount;
        [ObservableProperty] private int stockInCount;
        [ObservableProperty] private int stockOutCount;

        // --- فلترة ---
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterLogs();
            }
        }

        public ObservableCollection<string> LogTypesList { get; } = new() { "All Types", "Stock In", "Stock Out" };

        private string _selectedLogType = "All Types";
        public string SelectedLogType
        {
            get => _selectedLogType;
            set
            {
                SetProperty(ref _selectedLogType, value);
                FilterLogs();
            }
        }

        public InventoryLogViewModel(IGetAllService<InventoryLog> getAllService)
        {
            _getAllService = getAllService;
            _ = LoadLogs();
        }

        private async Task LoadLogs()
        {
            IsBusy = true;
            try
            {
                var list = await _getAllService.GetAllAsync();
                _allLogs = list.OrderByDescending(x => x.LogDate).ToList();

                CalculateStats();
                FilterLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FilterLogs()
        {
            var query = _allLogs.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var lowerSearch = SearchText.ToLower();
                query = query.Where(l =>
                    (l.ProductName?.ToLower().Contains(lowerSearch) ?? false) ||
                    (l.Reason?.ToLower().Contains(lowerSearch) ?? false) ||
                    (l.FullName?.ToLower().Contains(lowerSearch) ?? false)
                );
            }

            if (!string.IsNullOrEmpty(SelectedLogType) && SelectedLogType != "All Types")
            {
                query = query.Where(l => l.LogType == SelectedLogType);
            }

            FilteredLogs = new ObservableCollection<InventoryLog>(query);
        }

        private void CalculateStats()
        {
            TotalLogsCount = _allLogs.Count;
            StockInCount = _allLogs.Count(x => x.LogType == "Stock In");
            StockOutCount = _allLogs.Count(x => x.LogType == "Stock Out");
        }

        [RelayCommand]
        private void OpenAdjustStockWindow()
        {
            ToggleMainOpacity(true);

            var app = (App)Application.Current;
            var adjustWindow = app._host.Services.GetRequiredService<StockAdjustmentView>();

            adjustWindow.Owner = Application.Current.MainWindow;
            adjustWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var result = adjustWindow.ShowDialog();

            ToggleMainOpacity(false);

            _ = LoadLogs();
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