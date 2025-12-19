using ClinicAppointment_System.Data;
using ClinicAppointment_System.enums;

namespace ClinicAppointment_System.Models.Payment;

public class CashPaymentTypeStrategy: IPaymentTypeStrategy
{
    public bool ProcessPayment(Invoice invoice, Patient patient)
    {
        invoice.State = InvoiceState.Pending;
        DataSeed.Invoices.Add(invoice);
        return true;
    }
}