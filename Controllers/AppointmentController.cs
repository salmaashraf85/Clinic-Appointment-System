using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment_System.Controllers;

public class AppointmentController : Controller
{
    // Admin and Doctor can do all operations in the Appointment controller
    // operations: Get Appointments, Reschedule, Block slot, approve , cancel
    // change Work Schedulel in ui -> slots
    public IActionResult Index()
    {
        return View();
    }
}