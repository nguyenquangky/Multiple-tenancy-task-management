using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MultiTenancyAzureAD.Main.Models
{
    public class UpdateTask
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Task Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        [Display(Name = "Task Description")]
        public string Desc { get; set; }
        [Display(Name = "Job Description")]
        public string JobDesc { get; set; }
        public List<SelectListItem> Departments { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
    }
}