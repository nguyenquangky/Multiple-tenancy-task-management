using System;
using System.ComponentModel.DataAnnotations;

namespace MultiTenancyAzureAD.Data
{
    public class Task
    {
        public Task() { }
        public Task(string name, string desc, Department department, string jobDescription, string userName)
        {
            this.Name = name;
            this.Description = desc;
            this.Department = department;
            this.JobDescription = jobDescription;
            this.CreatedBy = userName;
            this.CreatedTime = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JobDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual Department Department { get; set; }
    }
}