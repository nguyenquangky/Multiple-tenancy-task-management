using MultiTenancyAzureAD.Data;
using MultiTenancyAzureAD.Main.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiTenancyAzureAD.Main.Services
{
    public class TaskService: ITaskService
    {
        private readonly IGenericRepository<Task> taskRepo;
        private readonly IGenericRepository<Department> deptRepo;
        public TaskService(IGenericRepository<Task> taskRepo, IGenericRepository<Department> deptRepo)
        {
            this.taskRepo = taskRepo;
            this.deptRepo = deptRepo;
        }
        public TaskService(IGenericRepository<Task> taskRepo)
        {
            this.taskRepo = taskRepo;
        }


        public Task AddNewTask(string name, string description, int departmentId, string jobDescription, string userName)
        {
            Department d = deptRepo.GetAll().FirstOrDefault(r => r.Id == departmentId);
            Task t = new Task(name, description, d, jobDescription, userName);
            taskRepo.Insert(t);
            return t;
        }

        public bool DeleteTask(int id)
        {
            return taskRepo.Delete(id);
        }

        public List<Task> GetAllTasks()
        {
            List<Task> tasks = taskRepo.GetAll().ToList();
            return tasks;
        }

        public Task GetTask(int id)
        {
            return taskRepo.GetById(id);
        }

        public bool UpdateTask(int id, string name, string description, int departmentId, string jobDescription)
        {
            Department d = deptRepo.GetAll().FirstOrDefault(r => r.Id == departmentId);
            Task t = taskRepo.GetById(id);
            if (t == null)
            {
                throw new Exception("Task is not found");
            }
            t.Name = name; t.Description = description; t.Department = d;t.JobDescription = jobDescription;
            t.UpdateTime = DateTime.Now;
            return taskRepo.Update(t);
        }

        List<Department> ITaskService.GetAllDepartments()
        {
            return deptRepo.GetAll().ToList();
        }
    }
}