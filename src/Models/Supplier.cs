using System.ComponentModel.DataAnnotations;

namespace MiniiERP1.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact person is required")]
        public string ContactPerson { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}