using System.Runtime.InteropServices.JavaScript;
using ClinicAppointment_System.Models;
using ClinicAppointment_System.Models.Commands;
using ClinicAppointment_System.Models.Schedule;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointment_System.Controllers;

public class AppointmentController(IScheduleService service) : Controller
{
    private ICommand _command =null;
    // Admin and Doctor can do all operations in the Appointment controller
    // operations: Get Appointments, Reschedule, Block slot, approve , cancel

    
    public IActionResult GetAll()
    {
        var userRole = Roles.Admin;
        if (userRole == Roles.Admin)
        {
            var result = service.GetAllAppointment();
            return View(result);
        }
        else if(userRole == Roles.Doctor)
        {
            Guid DoctorId = Guid.NewGuid();
            var result = service.GetAppointmentsForDoctor(DoctorId);
            return View(new List<Doctor>{result});
        }

        return NotFound();
    }
    
    // cancel
    public IActionResult Cancel(Guid id)
    {
        _command = new ChangeAppointmentStateCommand(id,AppointmentSate.Canceled,service);
        var result = _command.Execute();
        return RedirectToAction(nameof(GetAll));

    }
    
    // approve
    public IActionResult Approve(Guid id)
    {
        _command = new ChangeAppointmentStateCommand(id,AppointmentSate.Approved,service);
        var result = _command.Execute();
        return RedirectToAction(nameof(GetAll));

    }

    public IActionResult ReSchedule(Guid id, DateTime start, DateTime end)
    {
        _command = new ReScheduleAppointmentCommand(service,id,start,end);
        var result = _command.Execute();
        return RedirectToAction(nameof(GetAll));

    }

    public IActionResult UnBlock(Guid id)
    {
        _command = new UnBlockCommand(service,id);
        var result = _command.Execute();
        return RedirectToAction(nameof(GetAll));

    }
    public IActionResult Block(Guid id)
    {
        _command = new BlockSlotCommand(service,id);
        var result = _command.Execute();
        return RedirectToAction(nameof(GetAll));

    }

    public IActionResult Delete(Guid id)
    {
        _command = new DeleteCommand(service, id);
        var result = _command.Execute();
        if (result)
        {
            TempData["AlertMessage"] = "Appointment deleted successfully.";
            TempData["AlertType"] = "success"; 
        }
        else
        {
            TempData["AlertMessage"] = "Failed to delete the appointment.";
            TempData["AlertType"] = "danger"; 
        }
        return RedirectToAction(nameof(GetAll));
    }
    
    
}