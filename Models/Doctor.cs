namespace ClinicAppointment_System.Models;

public class Doctor : User
{
    public List<string> Specialties { get; set; }
    public List<Appointment> Appointments { get; set; }
    
}