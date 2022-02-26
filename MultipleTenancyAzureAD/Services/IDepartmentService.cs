using MultiTenancyAzureAD.Data;
using MultiTenancyAzureAD.Main.Models;
using System.Collections.Generic;

namespace MultiTenancyAzureAD.Main.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
        Department AddNewDepartment(string name, string description);
        bool UpdateDepartment(int id, string name, string description);
        Department GetDepartment(int id);
        bool DeleteDepartment(int id);
    }
}