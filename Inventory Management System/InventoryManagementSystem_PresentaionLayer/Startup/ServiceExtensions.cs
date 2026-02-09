using InventoryManagementSystem_BusinessLayer.CategoryServices;
using InventoryManagementSystem_BusinessLayer.ProductServices;
using InventoryManagementSystem_BusinessLayer.SupplierServices;
using InventoryManagementSystem_BusinessLayer.UserServices;
using InventoryManagementSystem_DataAccessLayer;
using InventoryManagementSystem_DataAccessLayer.CategoreyRepository;
using InventoryManagementSystem_DataAccessLayer.ProductRepository;
using InventoryManagementSystem_DataAccessLayer.SupplierRepository;
using InventoryManagementSystem_DataAccessLayer.UserRepo;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using InventoryManagementSystem_PresentaionLayer.Category.ViewCategory;
using InventoryManagementSystem_PresentaionLayer.Category.ViewModeCategory;
using InventoryManagementSystem_PresentaionLayer.Global;
using InventoryManagementSystem_PresentaionLayer.MainSideBar;
using InventoryManagementSystem_PresentaionLayer.MainSideBar.ViewModelsSideBar;
using InventoryManagementSystem_PresentaionLayer.Products_W;
using InventoryManagementSystem_PresentaionLayer.Products_W.Products_V;
using InventoryManagementSystem_PresentaionLayer.Products_W.Products_w;
using InventoryManagementSystem_PresentaionLayer.User.ViewUser;
using InventoryManagementSystem_PresentaionLayer.UserWindwo.ViewModeles;
using InventoryManagementSystem_PresentaionLayer.ViewUser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _User = InventoryManagementSystem_Model.Models.User;
namespace InventoryManagementSystem_PresentaionLayer.Startup
{
    public static class ServiceExtensions
    {

        public static void ConfigureApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddRepositories();
            builder.Services.AddBusinessServices();
            builder.Services.AddPresentationLayer();
            builder.Services.Configure<DataAccessSettings>(builder.Configuration.GetSection("ConnectionStrings"));

        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {

        
            //User Repositories
            services.AddTransient<IAddRepository<_User>, AddUser>();
            services.AddTransient<IGetAllRepository<_User>, GetAllUser>();
            services.AddTransient<IGetByIDRepository<_User>, GetUserByID>();
            services.AddTransient<IUpdateRepository<_User>, UpdateUser>();
            services.AddTransient<IDeleteRepository<int>, DeleteUser>();
            services.AddTransient<IGetUserByUserNameAndPassword, GetUserByUserNameAndPassword>();

            //Product Repositories
            services.AddTransient<IAddRepository<ProductDTO>, AddProductRepostiory>();
            services.AddTransient<IGetAllRepository<ProductDTO>, GetAllProductsRepository>();


            //Category Repository
            services.AddTransient<IGetAllRepository<CategoryDTO>, GetAllCategoreiesRepository>();
            services.AddTransient<IAddRepository<CategoryDTO>, AddCategoryRepostiory>();


            //Supplier Repostiory
            services.AddTransient<IGetAllRepository<SupplierDTO>, GetAllSuppliersRepository>();


            return services;
        }

        private static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // --- User Services ---
            services.AddTransient<IAddService<_User>, AddUserService>();
            services.AddTransient<IDeleteService, DeleteUserService>();
            services.AddTransient<IGetAllService<_User>, GetAllUsersService>();
            services.AddTransient<IGetByIDService<_User>, GetUserByIDService>(); 
            services.AddTransient<IUpdateService<_User>, UpdateUserService>();
            services.AddTransient<IGetByEmailAndPasswordService, GetUserByEmailAndPasswordService>();


            // --- Product Services  ---
            services.AddTransient<IAddService<ProductDTO>, AddProductService>();
            services.AddTransient<IGetAllService<ProductDTO>, GetAllProductsService>();


            // -- Category Services --
            services.AddTransient<IGetAllService<CategoryDTO>, GetAllCategoreiesService>();
            services.AddTransient<IAddService<CategoryDTO>, AddCategoryService>();

            //-- Supplier Serivces --
            services.AddTransient<IGetAllService<SupplierDTO>, GetAllSuppliersService>();

            return services;
        }

        private static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        {
            // ViewModels
            services.AddTransient<AddUserViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<SideBarViewModel>();
            services.AddTransient<AddProductViewModel>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<Product_w1ViewModel>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<AddEditCategoryViewModel>();

            // Windows
            services.AddTransient<UserWindow>();
            services.AddTransient<UserSingnUp>();
            services.AddTransient<UserSingin>();

            services.AddTransient<Window1>();
            services.AddTransient<Product_w1>();
            services.AddTransient<AddEditProducts>();

            services.AddTransient<CategoryWindow>();
            services.AddTransient<AddEditCategoryWindow>();

            return services;
        }
    }
}

