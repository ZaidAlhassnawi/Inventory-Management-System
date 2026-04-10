

using InventoryManagementSystem_Model.Models;

namespace InventoryManagementSystem_Model.Interfaces
{
    public interface IAddService<T>
    {
        Task<bool> AddAsync(T obj);
    }

    public interface IUpdateService<T>
    {
        Task<bool> UpdateAsync(T obj);
    }

    public interface IDeleteService<T>
    {
        Task<bool> DeleteAsync(int ID);
    }

    public interface IGetAllService<T>
    {
        Task<List<T>> GetAllAsync();
    }

    public interface IGetByIDService<T>
    {
        Task<T> FindAsync(int ID);
    }

    public interface IGetByEmailAndPasswordService
    {
        Task<User> FindAsync(string Email);
    }

    public interface IIsExistsService<T>
    {
        Task<bool> IsExistsAsync(int ID);
    }


}
