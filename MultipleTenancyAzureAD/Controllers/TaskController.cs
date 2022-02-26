using MultipleTenancyAzureAD.Core;
using MultiTenancyAzureAD.Data;
using MultiTenancyAzureAD.Main.Models;
using MultiTenancyAzureAD.Main.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MultiTenancyAzureAD.Main.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;
        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }
        private List<SelectListItem> GetDepartmentList()
        {
            return taskService.GetAllDepartments()
                    .Select(r => new SelectListItem()
                    {
                        Text = r.Name,
                        Value = r.Id.ToString()
                    }).ToList();
        }

        // GET: Task
        public ActionResult Index()
        {
            List<Task> tasks = taskService.GetAllTasks();
            return View(tasks);
        }

        [HttpGet]
        public ActionResult Add()
        {
            AddTask t = new AddTask();
            t.Departments = GetDepartmentList();

            return View(t);
        }

        [HttpPost]
        public ActionResult Add(AddTask task)
        {
            if (ModelState.IsValid)
            {
                Task t = taskService.AddNewTask(task.Name, task.Desc, task.DepartmentId, task.JobDesc, TenantHelper.UserName);

                TempData["AlertMessage"] = $"Task {t.Id} is added";
                return RedirectToAction("Index");
            }
            else
            {
                task.Departments = GetDepartmentList();
                return View(task);
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Task t = taskService.GetTask(id);
            UpdateTask task = new UpdateTask()
            {
                Name = t.Name,
                Desc = t.Description,
                DepartmentId = t.Department.Id,
                Departments = GetDepartmentList(),
                JobDesc = t.JobDescription,
                CreatedBy = t.CreatedBy,
                CreatedTime = t.CreatedTime,
                UpdatedTime = t.UpdateTime  
            };

            return View(task);
        }

        [HttpPost]
        public ActionResult Update(UpdateTask item)
        {
            if (ModelState.IsValid)
            {
                taskService.UpdateTask(item.Id, item.Name, item.Desc, item.DepartmentId, item.JobDesc);

                TempData["AlertMessage"] = $"Task {item.Id} is updated";
                return RedirectToAction("Index");
            }
            else
            {
                item.Departments = GetDepartmentList();
                return View(item);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            taskService.DeleteTask(id);
            TempData["AlertMessage"] = $"Task {id} is deleted";

            return RedirectToAction("Index");
        }
    }
}