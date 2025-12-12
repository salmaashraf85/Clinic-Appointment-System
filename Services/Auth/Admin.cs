using ClinicAppointment_System.Models;

namespace ClinicAppointment_System.Services.Auth
{
    public class Admin : IUser
    {
        public User AdminData { get; set; } = new User();
        public void RegisterUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = AdminData.FirstName,
                LastName = AdminData.LastName,
                Email = AdminData.Email,
                Password = AdminData.Password,
                Role = Roles.Admin,
                CreatedAt = DateTime.Now
            };
        }
    }
}