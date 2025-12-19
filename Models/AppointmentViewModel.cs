namespace ClinicAppointment_System.Models;

public class AppointmentViewModel
{
    public Guid AppointmentId { get; set; }
    public string DoctorName { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public string Day => StartTime.DayOfWeek.ToString();
}