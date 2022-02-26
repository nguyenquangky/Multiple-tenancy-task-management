using MultiTenancyAzureAD.Data;
using MultiTenancyAzureAD.Main.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenancyAzureAD.Main.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly IGenericRepository<Department> departmentRepo;
        public DepartmentService(IGenericRepository<Department> departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }

        public Department AddNewDepartment(string name, string description)
        {
            Department d = new Department(name, description);
            departmentRepo.Insert(d);
            return d;
        }

        public bool UpdateDepartment(int id, string name, string description)
        {
            Department d = departmentRepo.GetById(id);
            if(d == null)
            {
                throw new InvalidOperationException("Invalid department");
            }
            d.Name = name;d.Description = description;
            return departmentRepo.Update(d);
        }

        public List<Department> GetAllDepartments()
        {
            return departmentRepo.GetAll().ToList();
        }

        public Department GetDepartment(int id)
        {
            return departmentRepo.GetById(id);
        }

        public bool DeleteDepartment(int id)
        {
            Department d = GetDepartment(id);
            if(d == null)
            {
                throw new InvalidOperationException("Department is not found");
            }
            if (d.Tasks != null && d.Tasks.Any())
            {
                throw new InvalidOperationException("There are relavant tasks of this department, please delete them before delete this department");
            }
            return departmentRepo.Delete(id);
        }
    }
}