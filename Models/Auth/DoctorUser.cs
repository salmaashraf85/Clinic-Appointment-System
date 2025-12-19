using ClinicAppointment_System.Models;

public class DoctorUser : IUser
{
    public User RegisterUser(User data)
    {
        return new Doctor
        {
            Id = Guid.NewGuid(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Password = data.Password,
            CreatedAt = DateTime.Now,
            // Specialties = new List<string>(),
            // Appointments = new List<Appointment>()
        };
    }


}