using ClinicAppointment_System.Services.Auth;

namespace ClinicAppointment_System.Models;

namespace ClinicAppointment_System.Services.Auth
{
    public class Patient : IUser
    {
        public Patient PatientData { get; set; } = new Patient();

        public void RegisterUser()
        {
            return new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = PatientData.FirstName,
                LastName = PatientData.LastName,
                Email = PatientData.Email,
                Password = PatientData.Password,
                Role = Roles.Patient,
                Appointments = new List<Appointment>(),
                CreatedAt = DateTime.Now
            };
        }
    }
}