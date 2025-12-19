namespace ClinicAppointment_System.Models.Payment;

public interface IPaymentTypeStrategy
{
    bool ProcessPayment(Invoice invoice, Patient patient);
}