using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models.Schedule;

public interface IScheduleService
{
    public void UpdateAppointment(DoctorSchedule app);
    public void DeleteAppointment(DoctorSchedule app);
    public bool BlockAppointmentSlot(Guid appId);
    public void UnblockAppointmentSlot(Guid appId);
    public void ReScheduleAppointmentSlot(Guid appId, DateTime start, DateTime end);
    public void UpdateState(Guid appId, AppointmentSate state);
    public DoctorSchedule GetAppointmentSlot(Guid appId);
    public List<Doctor> GetAllAppointment();
    public Doctor GetAppointmentsForDoctor(Guid doctorId);
    public void AddAppointment(DoctorSchedule app);
    
    
}