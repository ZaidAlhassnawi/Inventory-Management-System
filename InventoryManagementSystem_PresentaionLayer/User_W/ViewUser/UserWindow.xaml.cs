using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagementSystem_PresentaionLayer.ViewUser
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            var userSingnin = app._host.Services.GetRequiredService<UserSingin>();
            this.MainFrame.Navigate(userSingnin);
        }

        #region Move Window
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion


    }
}
