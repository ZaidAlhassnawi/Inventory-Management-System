using CommunityToolkit.Mvvm.ComponentModel; // للـ ObservableObject
using CommunityToolkit.Mvvm.Input;          // للـ RelayCommand
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_PresentaionLayer.Global;
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using InventoryManagementSystem_PresentaionLayer.User.ViewUser;
using InventoryManagementSystem_PresentaionLayer.ViewUser;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using _User = InventoryManagementSystem_Model.Models.User;

namespace InventoryManagementSystem_PresentaionLayer.UserWindwo.ViewModeles
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IGetByEmailAndPasswordService _userSerivce;
        private readonly IPasswordHasher _passwordHasher;

        private _User _currentUser;

        public _User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel(IGetByEmailAndPasswordService UserService, IPasswordHasher passwordHasher)
        {
            _userSerivce = UserService;

            _passwordHasher = passwordHasher;

            CurrentUser = new _User();

        }
        
        [RelayCommand]
        private async Task Login(object parameter)
        {
            // 1. استلام صندوق الباسورد الموجود داخل الصفحة (Page)
            if (parameter is not PasswordBox passwordBox) return;

            

            var user = await _userSerivce.FindAsync(CurrentUser.Email);

            if (user == null)
                return;

            if (user != null)
            {
                if (!_passwordHasher.VerifyPassword(passwordBox.Password, user.Password))
                {
                    MessageBox.Show("Credintal is wrong. Password or Email wrong", "Credintal Error", MessageBoxButton.OK);
                    return;
                }
            }


            Global.Global.CurrentUser = user;

            // 2. هذه الدالة ستبحث عن "الأب" لهذا الصندوق.
            // ستكتشف أن الصندوق في Page، والـ Page في Window.. فتقوم بجلب الـ Window.
            var parentWindow = Window.GetWindow(passwordBox);

            var app = (App)Application.Current;
            var Win = app._host.Services.GetRequiredService<Window1>();

            // 3. إغلاق النافذة الأم
            parentWindow?.Close();

            Win.ShowDialog();
                
            
        }


        [RelayCommand]
        private void OpenSignup(object parameter) // نستقبل البارامتر كـ object
        {
            // 1. التأكد أن البارامتر هو صفحة
            if (parameter is not Page currentPage) return;

            // 2. الحصول على النافذة الرئيسية وتحويلها لـ MainWindow للوصول للـ Frame
            var userWindow = Window.GetWindow(currentPage) as UserWindow;

            if (userWindow != null)
            {
                // 3. جلب صفحة التسجيل من السرفيس
                var app = (App)Application.Current;
                var signupPage = app._host.Services.GetRequiredService<UserSingnUp>();

                // 4. التنقل (لا حاجة لـ Hide)
                userWindow.MainFrame.Navigate(signupPage);
            }
        }

    }
}
