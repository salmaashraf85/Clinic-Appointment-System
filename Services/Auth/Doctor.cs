using ClinicAppointment_System.Services.Auth;

namespace ClinicAppointment_System.Models;

namespace ClinicAppointment_System.Services.Auth
{
    public class Doctor : IUser
    {
        public Doctor DoctorData { get; set; } = new Doctor();
        public void RegisterUser()
        {
            return new Doctor
            {
                Id = Guid.NewGuid(),
                FirstName = DoctorData.FirstName,
                LastName = DoctorData.LastName,
                Email = DoctorData.Email,
                Password = DoctorData.Password,
                Role = Roles.Doctor,
                Specialties = DoctorData.Specialties,
                Appointments = new List<Appointment>(),
                CreatedAt = DateTime.Now
            };

        }
    }
}