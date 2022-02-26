using MultiTenancyAzureAD.Data;
using System.Collections.Generic;

namespace MultiTenancyAzureAD.Main.Services
{
    public interface ITaskService
    {
        List<Task> GetAllTasks();
        List<Department> GetAllDepartments();
        Task AddNewTask(string name, string description, int departmentId, string jobDescription, string userName);
        bool UpdateTask(int id, string name, string description, int departmentId, string jobDescription);
        Task GetTask(int id);
        bool DeleteTask(int id);
    }
}