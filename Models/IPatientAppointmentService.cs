using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models;

public interface IPatientAppointmentService
{
    List<DoctorSchedule> GetAvailableAppointments();
    DoctorSchedule BookAppointment(Guid appointmentId, Guid patientId);
}
