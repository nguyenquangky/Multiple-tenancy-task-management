using System.ComponentModel.DataAnnotations;

namespace MultiTenancyAzureAD.Main.Models
{
    public class AddDepartment
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}