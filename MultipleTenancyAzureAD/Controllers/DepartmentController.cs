using MultiTenancyAzureAD.Data;
using MultiTenancyAzureAD.Main.Models;
using MultiTenancyAzureAD.Main.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiTenancyAzureAD.Main.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;
        public DepartmentController(IDepartmentService departmentService) 
        {
            this.departmentService = departmentService;
        }
        // GET: Department
        public ActionResult Index()
        {
            List<Department> depts = departmentService.GetAllDepartments();
            return View(depts);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddDepartment item)
        {
            Department d = departmentService.AddNewDepartment(item.Name, item.Description);
            TempData["AlertMessage"] = $"Department {d.Id} is added";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Department d = departmentService.GetDepartment(id);
            UpdateDepartment updateDepartment = new UpdateDepartment()
            {
                Name = d.Name,
                Description = d.Description
            };

            return View(updateDepartment);
        }

        [HttpPost]
        public ActionResult Update(UpdateDepartment item)
        {
            departmentService.UpdateDepartment(item.Id, item.Name, item.Description);

            TempData["AlertMessage"] = $"Department {item.Id} is updated";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                departmentService.DeleteDepartment(id);
                TempData["AlertMessage"] = $"Department {id} is deleted";

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrMsg"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}