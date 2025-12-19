using ClinicAppointment_System.enums;

namespace ClinicAppointment_System.Models.Payment;

public class PaymentManager
{
    private IPaymentTypeStrategy _strategy=null;

    public Invoice CreateInvoice(Guid appointmentId)
    {
        return new Invoice()
        {
            Id = Guid.NewGuid(),
            AppointmetnId = appointmentId,
            State = InvoiceState.Pending
        };
    }
    public bool Checkout(Guid appointmentId,Patient patient, PaymentType paymentType)
    {
        Invoice invoice = CreateInvoice(appointmentId);
        if (paymentType == PaymentType.Cash)
        {
            _strategy = new WalletPaymentTypeStrategy();
        }
        else if(paymentType == PaymentType.Wallet)
        {
            _strategy = new CashPaymentTypeStrategy(); 
            
        }
        bool success = _strategy.ProcessPayment(invoice, patient);
        return success;
    }
}