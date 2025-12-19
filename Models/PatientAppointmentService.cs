using ClinicAppointment_System.enums;
using ClinicAppointment_System.Models.Entittes;
using ClinicAppointment_System.Models.Payment;

namespace ClinicAppointment_System.Models;

public class PatientAppointmentService : IPatientAppointmentService
{
    private readonly AppointmentScheduler _scheduler;

    public PatientAppointmentService()
    {
        _scheduler = AppointmentScheduler.Instance;
    }

    public List<DoctorSchedule> GetAvailableAppointments()
    {
        return _scheduler.GetAll()
            .Where(a => a.IsAavailable && !a.IsBlocked)
            .ToList();
    }

    public DoctorSchedule BookAppointment(Guid appointmentId, Guid patientId)
    {
        var appointment = _scheduler.GetAll()
            .FirstOrDefault(a => a.Id == appointmentId);

        if (appointment == null)
            throw new Exception("Appointment not found");

        if (!appointment.IsAavailable || appointment.IsBlocked)
            throw new Exception("Appointment not available");

        var patientHasConflict = _scheduler.GetAll().Any(a =>
            a.PatientId == patientId &&
            a.StartTime < appointment.EndTime &&
            appointment.StartTime < a.EndTime
        );

        if (patientHasConflict)
            throw new Exception("Patient already has an appointment at this time");

        
        // var hasActiveAppointment = _scheduler.GetAll().Any(a =>
        //     a.PatientId == patientId &&
        //     a.AppointmentSate == AppointmentSate.Pending
        // );
        //
        // if (hasActiveAppointment)
        //     throw new Exception("Patient already has an active appointment");
        appointment.PatientId = patientId;
        appointment.IsAavailable = false;
        appointment.AppointmentSate = AppointmentSate.Pending;

        return appointment;
    }

    public bool PayAppointment(Guid appointmentId, Patient patient, PaymentType paymentType)
    {
        var paymentManager = new PaymentManager();
        var result = paymentManager.Checkout(appointmentId,patient, paymentType);
        return result;
    }
}
