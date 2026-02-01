using System.ComponentModel.DataAnnotations;

namespace MiniiERP1.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Module name is required")]
        [StringLength(100, ErrorMessage = "Module name must be less than 100 characters")]
        public string Module { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Role ID is required")]
        public int RoleId { get; set; }
        
        public Role.UserRole Role { get; set; }
    }
}