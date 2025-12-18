namespace ClinicAppointment_System.Models.Entittes;

public class DoctorSchedule
{
    public Guid Id { get; set; }
    public Guid DoctorId;
    public Guid? PatientId;
    public String Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAavailable { get; set; } = true;
    public bool IsBlocked { get; set; } = false;
    public AppointmentSate AppointmentSate { get; set; } = AppointmentSate.Pending;

}