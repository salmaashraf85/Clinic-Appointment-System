using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models;

public class Patient: User
{
    public List<DoctorSchedule> Appointments;

}