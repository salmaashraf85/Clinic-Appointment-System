using ClinicAppointment_System.Models;

namespace ClinicAppointment_System.Services.Auth
{
    public class UserFactory
    {
        public IUser CreateUser(Models.Roles role)
        {
            switch (role)
            {
                case Models.Roles.Admin:
                    return new Admin();
                case Models.Roles.Doctor:
                    return new Doctor();
                case Models.Roles.Patient:
                    return new Patient();
                default:
                    throw new ArgumentException("Invalid role");
            }
        }
    }
}