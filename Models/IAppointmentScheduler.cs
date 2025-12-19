using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models;

public interface IAppointmentScheduler
{
    bool IsAvailable(Guid doctorId, DateTime startTime, DateTime endTime);
    void Book(DoctorSchedule appointment);
    List<DoctorSchedule> GetAll();
}
