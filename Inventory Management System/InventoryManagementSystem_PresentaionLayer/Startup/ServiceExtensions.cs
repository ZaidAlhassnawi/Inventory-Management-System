using InventoryManagementSystem_BusinessLayer.CategoryServices;
using InventoryManagementSystem_BusinessLayer.ProductServices;
using InventoryManagementSystem_BusinessLayer.UserServices;
using InventoryManagementSystem_DataAccessLayer.CategoreyRepository;
using InventoryManagementSystem_DataAccessLayer.ProductRepository;
using InventoryManagementSystem_DataAccessLayer.UserRepo;
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
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


            //Category Repository
            services.AddTransient<IGetAllRepository<CategoryDTO>, GetAllCategoreiesRepository>();


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


            // -- Category Services --
            services.AddTransient<IGetAllService<CategoryDTO>, GetAllCategoreiesService>();

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

            // Windows
            services.AddTransient<UserWindow>();
            services.AddTransient<UserSingnUp>();
            services.AddTransient<UserSingin>();

            services.AddTransient<Window1>();
            services.AddTransient<Product_w1>();
            services.AddTransient<AddEditProducts>();

            return services;
        }
    }
}

