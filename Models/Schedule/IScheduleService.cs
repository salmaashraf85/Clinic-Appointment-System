using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models.Schedule;

public interface IScheduleService
{
    public void UpdateAppointment(DoctorSchedule app);
    public void DeleteAppointment(DoctorSchedule app);
    public void BlockAppointmentSlot(Guid appId);
    public void UnblockAppointmentSlot(Guid appId);
    public void ReScheduleAppointmentSlot(Guid appId, TimeSpan start, TimeSpan end);
    public void UpdateState(Guid appId, AppointmentSate state);
}