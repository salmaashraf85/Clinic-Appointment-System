using ClinicAppointment_System.Data;
using ClinicAppointment_System.enums;

namespace ClinicAppointment_System.Controllers;

using ClinicAppointment_System.Models;
using Microsoft.AspNetCore.Mvc;

public class PatientAppointmentController : Controller
{
    private readonly IPatientAppointmentService _service;

    public PatientAppointmentController()
    {
        _service = new PatientAppointmentService();
    }

    [HttpGet]
    public IActionResult Index()
    {
        var appointments = _service.GetAvailableAppointments();

        var model = appointments.Select(a =>
        {
            var doctor = DataSeed.Doctors.First(d => d.Id == a.DoctorId);

            return new AppointmentViewModel
            {
                AppointmentId = a.Id,
                DoctorName = $"{doctor.DoctorInfo.FirstName} {doctor.DoctorInfo.LastName}",
                StartTime = a.StartTime,
                EndTime = a.EndTime
            };
        }).ToList();

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Book(Guid appointmentId)
    {
        try
        {
            var currentUser = DataSeed.GetCurrentUser();
            var patient = DataSeed.Patients
                .FirstOrDefault(p => p.Email == currentUser.Email);

            if (patient == null)
                return Unauthorized();

            var appointment = _service.BookAppointment(appointmentId, patient.Id);
            return RedirectToAction("Confirmation", new { id = appointment.Id });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    public IActionResult Confirmation(Guid id)
    {
        var appointment = AppointmentScheduler.Instance
            .GetAll()
            .FirstOrDefault(a => a.Id == id);

        var doctor = DataSeed.Doctors
            .FirstOrDefault(d => d.Id == appointment.DoctorId);

        var model = new AppointmentViewModel
        {
            AppointmentId = appointment.Id,
            DoctorName = $"{doctor.DoctorInfo.FirstName} {doctor.DoctorInfo.LastName}",
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime
        };

        return View(model);
    }

    public IActionResult Pay(Guid appointmentId,  string paymentMethod)
    {
        var appointment = AppointmentScheduler.Instance
            .GetAll()
            .FirstOrDefault(a => a.Id == appointmentId);

        var currentUser = DataSeed.GetCurrentUser();
        var patient = DataSeed.Patients
            .FirstOrDefault(p => p.Email == currentUser.Email);

        if (patient == null)
            return Unauthorized();
        
        if (appointment == null ) return BadRequest();
        var result = false;
        
        if(paymentMethod=="Wallet")
            result = _service.PayAppointment(appointmentId,patient,PaymentType.Wallet);
        else if (paymentMethod == "Cach")
            result = _service.PayAppointment(appointmentId, patient, PaymentType.Wallet);
        
        if (result)
        {
            ViewData["Message"] = "Your appointment has been Confirmed Successfully";
        }
        else
        {
            ViewData["Message"] = "Failed to Confirm Appointment";
        }

        return RedirectToAction("MyAppointments");
    }
    
    public IActionResult MyAppointments()
    {
        var currentUser = DataSeed.GetCurrentUser();
        var patient = DataSeed.Patients
            .FirstOrDefault(p => p.Email ==currentUser.Email);

        if (patient == null)
            return Unauthorized();

        var appointments = AppointmentScheduler.Instance
            .GetAll()
            .Where(a => a.PatientId == patient.Id)
            .ToList();

        return View(appointments);
    }

}