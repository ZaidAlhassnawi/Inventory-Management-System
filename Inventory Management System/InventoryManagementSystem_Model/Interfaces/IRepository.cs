using InventoryManagementSystem_Model.Models;

namespace InventoryManagementSystem_Model.Interfaces
{
    /// <summary>
    /// Defines a repository interface for asynchronously retrieving all entities of a specified type from a data
    /// source.InventoryDB
    /// </summary>
    /// <typeparam name="T">The type of entities to retrieve.</typeparam>
    public interface IGetAllRepository<T>
    {
        Task<List<T>> GetAllAsync();
    }

    /// <summary>
    /// Defines a repository interface for asynchronously adding entities to a data store.
    /// </summary>
    /// <typeparam name="T">The type of entity to add.</typeparam>
    public interface IAddRepository<T>
    {
        Task<int> AddAsync(T obj);
    }

    /// <summary>
    /// Defines a repository interface for deleting entities of type T asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of entity to be deleted.</typeparam>
    public interface IDeleteRepository<T>
    {
        Task<bool> DelteAsync(int ID);
    }

    /// <summary>
    /// Defines a repository interface for updating entities asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of entity to update.</typeparam>
    public interface IUpdateRepository<T>
    {
        Task<bool> UpdateAsync(T obj);
    }

    /// <summary>
    /// Defines a repository interface for retrieving an entity by its ID asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the entity to retrieve.</typeparam>
    public interface IGetByIDRepository<T>
    {
        Task<T> GetByIDAsync(int ID);
    }


    public interface IGetUserByUserNameAndPassword
    {
        Task<User> FindAsync(string Email);
    }
    
}
