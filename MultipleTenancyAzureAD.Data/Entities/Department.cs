using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MultiTenancyAzureAD.Data
{
    public class Department
    {
        public Department() { }
        public Department(string name, string desc)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name is required");
            }
            this.Name = name;
            this.Description = desc;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Task> Tasks { get; private set; }
    }
}