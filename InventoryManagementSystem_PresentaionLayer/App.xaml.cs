using InventoryManagementSystem_PresentaionLayer.Startup;
using InventoryManagementSystem_PresentaionLayer.ViewUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace InventoryManagementSystem_PresentaionLayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly IHost _host;

        public App()
        {
            var builder = Host.CreateApplicationBuilder();

           
            builder.ConfigureApplicationServices();

            _host = builder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<UserWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"حدث خطأ غير متوقع: {e.Exception.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }
    }
}


