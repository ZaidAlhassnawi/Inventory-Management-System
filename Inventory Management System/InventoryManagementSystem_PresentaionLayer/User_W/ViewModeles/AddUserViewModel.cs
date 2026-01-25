using CommunityToolkit.Mvvm.ComponentModel; // للـ ObservableObject
using CommunityToolkit.Mvvm.Input;          // للـ RelayCommand
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_PresentaionLayer.Global;
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using InventoryManagementSystem_PresentaionLayer.ViewUser;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using _User = InventoryManagementSystem_Model.Models.User;

namespace InventoryManagementSystem_PresentaionLayer.UserWindwo.ViewModeles
{
    public partial class AddUserViewModel : ObservableValidator
    {
        private readonly IAddService<_User> _Add;
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

        
        [ObservableProperty]
        [Required(ErrorMessage = "Full Name is required")]
        [MinLength(6, ErrorMessage = "Name must be at least 6 characters")]
        [NotifyDataErrorInfo] 
        private string fullName;

        [ObservableProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")] 
        [NotifyDataErrorInfo]
        private string email;

        [ObservableProperty]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [NotifyDataErrorInfo]
        private string password;

        [ObservableProperty]
        [Required(ErrorMessage = "Confirm Password is required")]
        [property:Compare(nameof(Password), ErrorMessage = "Passwords do not match")] 
        [NotifyDataErrorInfo]
        private string confirmPassword;

        [ObservableProperty]
        private int roleId; 

        public AddUserViewModel(IAddService<_User> add, IGetByEmailAndPasswordService UserService,IPasswordHasher passwordHasher)
        {
            _Add = add;

            _passwordHasher = passwordHasher;

            _userSerivce = UserService;

            CurrentUser = new _User();
        }

        partial void OnPasswordChanged(string value)
        {
            if (!string.IsNullOrEmpty(ConfirmPassword))
            {
                ValidateProperty(ConfirmPassword, nameof(ConfirmPassword));
            }
        }

        [RelayCommand]
        private async Task SignUp(object parameter)
        {
            // 1. التحقق من وجود أخطاء قبل الحفظ
            ValidateAllProperties(); // فحص كل الحقول
            if (HasErrors)
            {
                var message = string.Join("\n", GetErrors().Select(e => e.ErrorMessage));

                // اعرضها لنعرف المشكلة
                MessageBox.Show($"الأخطاء الموجودة:\n{message}", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (parameter is not PasswordBox passwordBox) return;


            try
            {
                string hashedPassword = _passwordHasher.HashPassword(Password);

                var newUser = new _User
                {
                    FullName = FullName,
                    Email = Email,
                    Password = hashedPassword, 
                    RollID = RoleId 
                };

                bool isSaved = await _Add.AddAsync(newUser);

                if(!isSaved)
                {
                    MessageBox.Show("User is not Added. An Error Occure", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 2. هذه الدالة ستبحث عن "الأب" لهذا الصندوق.
                // ستكتشف أن الصندوق في Page، والـ Page في Window.. فتقوم بجلب الـ Window.
                var parentWindow = Window.GetWindow(passwordBox);

                var app = (App)Application.Current;
                var Win = app._host.Services.GetRequiredService<Window1>();

                Global.Global.CurrentUser = newUser;


                // 3. إغلاق النافذة الأم
                parentWindow?.Close();

                Win.ShowDialog();

            }
            catch (Exception ex)
            {
                // معالجة الأخطاء
            }
        }

        [RelayCommand]
        private void OpenSignIn(object parameter) 
        {
            if (parameter is not Page currentPage) return;

            var userWindow = Window.GetWindow(currentPage) as UserWindow;

            if (userWindow != null)
            {
                var app = (App)Application.Current;
                var signupPage = app._host.Services.GetRequiredService<UserSingin>();
                userWindow.MainFrame.Navigate(signupPage);
            }
        }
    }
}
