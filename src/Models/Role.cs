using System.ComponentModel.DataAnnotations;

namespace MiniiERP1.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        [StringLength(50, ErrorMessage = "Role name must be less than 50 characters")]
        public string Name { get; set; } = string.Empty;
        
        public enum UserRole
        {
            Administrator,
            OperationUser
        }
    }
}