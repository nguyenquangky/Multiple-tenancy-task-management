using System.ComponentModel.DataAnnotations;

namespace MultiTenancyAzureAD.Main.Models
{
    public class UpdateDepartment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}