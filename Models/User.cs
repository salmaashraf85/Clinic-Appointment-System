using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment_System.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]

    public string Email { get; set; }
    [Required]

    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}