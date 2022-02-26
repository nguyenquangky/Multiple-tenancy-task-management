using System.Collections.Generic;

namespace MultiTenancyAzureAD.Main.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(int id);
    }
}