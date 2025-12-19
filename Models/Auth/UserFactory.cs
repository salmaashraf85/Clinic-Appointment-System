using ClinicAppointment_System.Models;

public class UserFactory
{
    public IUser CreateUser(Roles role)
    {
        return role switch
        {
            Roles.Doctor => new DoctorUser(),
            Roles.Patient => new PatientUser(),
            Roles.Admin => new Admin(),
            _ => throw new ArgumentException("Invalid role"),
        };
    }
}