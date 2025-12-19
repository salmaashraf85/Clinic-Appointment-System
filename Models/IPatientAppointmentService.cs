using ClinicAppointment_System.enums;
using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models;

public interface IPatientAppointmentService
{
    List<DoctorSchedule> GetAvailableAppointments();
    DoctorSchedule BookAppointment(Guid appointmentId, Guid patientId);
    public bool PayAppointment(Guid appointmentId, Patient patient, PaymentType paymentType);
}
