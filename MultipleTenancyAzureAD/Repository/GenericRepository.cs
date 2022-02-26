using MultiTenancyAzureAD.Main.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MultiTenancyAzureAD.Main.Repository
{
  
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _unitOfWork.Context.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public TEntity GetById(int id)
        {
            try
            {
                return _unitOfWork.Context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public TEntity Get(TEntity entity)
        {
            try
            {
                return _unitOfWork.Context.Set<TEntity>().Find(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public bool Insert(TEntity entity)
        {
            try
            {
                _unitOfWork.Context.Set<TEntity>().Add(entity);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _unitOfWork.Context.Set<TEntity>().Attach(entity);
                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                var entityToRemove = Get(entity);
                _unitOfWork.Context.Set<TEntity>().Remove(entityToRemove);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entityToRemove = GetById(id);
                _unitOfWork.Context.Set<TEntity>().Remove(entityToRemove);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}