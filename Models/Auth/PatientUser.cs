using ClinicAppointment_System.Models;

public class PatientUser : IUser
{
    public User RegisterUser(User data)
    {
        return new Patient
        {
            Id = Guid.NewGuid(),
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Password = data.Password,
            CreatedAt = DateTime.Now,
            Appointments = new List<Appointment>()
        };
    }


}