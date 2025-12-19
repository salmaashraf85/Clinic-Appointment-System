using ClinicAppointment_System.enums;
using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models.Payment;

public class Invoice
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public Guid AppointmetnId { get; set; }
    public InvoiceState  State { get; set; }
}