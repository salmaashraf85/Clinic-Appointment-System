using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment_System.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public Roles Role { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}