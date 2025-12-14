namespace ClinicAppointment_System.Models.Entittes;

public class DoctorSchedule
{
    public String Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
   public bool IsAavailable { get; set; }
}