using ClinicAppointment_System.Data;
using ClinicAppointment_System.enums;

namespace ClinicAppointment_System.Models.Payment;

public class WalletPaymentTypeStrategy: IPaymentTypeStrategy
{
    public bool ProcessPayment(Invoice invoice, Patient patient)
    {
        if (patient.Wallet < invoice.Price) return false;
        patient.Wallet -= invoice.Price;
        invoice.State = InvoiceState.Paid;
        DataSeed.Invoices.Add(invoice);
        return true;
    }
}