using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicAppointment_System.Models.Entittes;
using ClinicAppointment_System.Models;
using ClinicAppointment_System.Data;
namespace ClinicAppointment_System.Controllers;

public class DoctorController : Controller
{
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(DoctorViewModel model,string Role) {
        ModelState.Remove("Role");
        if (ModelState.IsValid)
        {
            var userPart = new User 
            { 
                Id = Guid.NewGuid(),
                FirstName = model.FirstName, 
                Email = model.Email, 
                Password = model.Password,
               LastName = model.LastName,
                Role = "Doctor"
            };

            var specialistPart = new DoctorSpecialist 
            { 
                Specialization = model.Specialization, 
                Department = model.Department, 
                YearsOfExperience = model.YearsOfExperience 
            };

         var domainSchedules = new List<DoctorSchedule>();
    
    if (model.Schedules != null)
    {
        foreach (var item in model.Schedules)
        {
            domainSchedules.Add(new DoctorSchedule
            {
                Day = item.Day,
                StartTime = item.StartTime,
                EndTime = item.EndTime
            });
        }
    }

            IdoctorBuilder builder = new DoctorBuilder();
            Doctor newDoctor = builder
                .BuilDoctorInfo(userPart)
                .BuildSpecialist(specialistPart)
                .BuildSchedule(domainSchedules)
                .Build();

            DataSeed.Doctors.Add(newDoctor);

           if(Role=="Admin"){
            return RedirectToAction("Index");
        }
        else if(Role=="Doctor"){
            return RedirectToAction("IndexDoctor");
        }
        else{
            return RedirectToAction("Index");
        }
        }

        return View(model);
    }
    public IActionResult Delete(Guid id)
    {
      var loggedInUser = new User { Id = id, Role = "Admin", FirstName = "Dr. Current" };
      try{
      IDoctorService proxy = new DoctorServiceProxy(loggedInUser);
      proxy.DeleteDoctor(id);
      if(loggedInUser.Role=="Admin"){
            return RedirectToAction("Index");
        }
        else if(loggedInUser.Role=="Doctor"){
            return RedirectToAction("IndexDoctor");
        }
        else{
            return RedirectToAction("Index");
        }
      }catch (UnauthorizedAccessException ex)
        {
            // ViewBag.ErrorMessage = ex.Message;
        TempData["ErrorMessage"] = "Access Denied: You cannot delete this profile.";
           if(loggedInUser.Role=="Admin"){
            return RedirectToAction("Index");
        }
        else if(loggedInUser.Role=="Doctor"){
            return RedirectToAction("IndexDoctor");
        }
        else{
            return RedirectToAction("Index");
        }
        }
    }
    [HttpGet]
public IActionResult Edit(Guid id)
{
    var doctor = DataSeed.Doctors.FirstOrDefault(d => d.DoctorInfo.Id == id);
     var loggedInUser = new User {Id = id, Role = "Admin" }; 

    if (doctor == null) return NotFound();
    try
    {
        DoctorServiceProxy service = new DoctorServiceProxy(loggedInUser);
          if(!service.CanAccess(id)){
            Console.WriteLine("Access Denied: You cannot edit this profile.");
            throw new UnauthorizedAccessException("Access Denied: You cannot edit this profile.");
        }
         
    var model = new DoctorViewModel
    {
        
        FirstName = doctor.DoctorInfo.FirstName,
        Email = doctor.DoctorInfo.Email,
        LastName = doctor.DoctorInfo.LastName,        
        Specialization = doctor.DoctorSpecialist.Specialization,
        Department = doctor.DoctorSpecialist.Department,
        YearsOfExperience = doctor.DoctorSpecialist.YearsOfExperience,
        Role = doctor.DoctorInfo.Role,

        Schedules = doctor.DoctorSchedule.Select(s => new ScheduleInput
        {
            Day = s.Day,
            StartTime = s.StartTime,
            EndTime = s.EndTime
        }).ToList()
    };

    ViewBag.DoctorId = id; 

    return View(model);
    }
    catch (UnauthorizedAccessException ex)
    {
        ViewBag.ErrorMessage = ex.Message;
        Console.WriteLine("ex.Message", ex.Message);
        TempData["ErrorMessage"] = "Access Denied: You cannot delete this profile.";
        ViewBag.DoctorId = id; 
         if(loggedInUser.Role=="Admin"){
            return RedirectToAction("Index");
        }
        else if(loggedInUser.Role=="Doctor"){
            return RedirectToAction("IndexDoctor");
        }
        else{
            return RedirectToAction("Index");
        }
    }
   
}

[HttpPost]
public IActionResult Edit(Guid id, DoctorViewModel model)
{ModelState.Remove("Password");
    if (!ModelState.IsValid){
         Console.WriteLine("ex.Messag not valid ");
         foreach (var modelStateKey in ModelState.Keys)
    {
        var modelStateVal = ModelState[modelStateKey];
        foreach (var error in modelStateVal.Errors)
        {
            Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
        }
    }
        ViewBag.DoctorId = id;
         return View(model);
    }

    try
    {
        var existingDoctor = DataSeed.Doctors.FirstOrDefault(d => d.DoctorInfo.Id == id);
        string passwordToSave = existingDoctor?.DoctorInfo.Password;
        var updatedSchedules = new List<DoctorSchedule>();
        if (model.Schedules != null)
        {
            foreach (var s in model.Schedules)
            {
                updatedSchedules.Add(new DoctorSchedule 
                { Day = s.Day, StartTime = s.StartTime, EndTime = s.EndTime });
            }
        }

        var newDoctorData = new Doctor
        {
            DoctorInfo = new User 
            { 
                FirstName = model.FirstName, 
                LastName = model.LastName,
                Email = model.Email, 
                Password= passwordToSave
            },
            DoctorSpecialist = new DoctorSpecialist 
            { 
                Specialization = model.Specialization, 
                Department = model.Department, 
                YearsOfExperience = model.YearsOfExperience 
            },
            DoctorSchedule = updatedSchedules
        };

        var loggedInUser = new User { Id = id, Role = "Admin" }; 
        IDoctorService service = new DoctorServiceProxy(loggedInUser);
         Console.WriteLine("newDoctorData", newDoctorData);

        service.EditDoctor(id, newDoctorData);
        if(loggedInUser.Role=="Admin"){
            return RedirectToAction("Index");
        }
        else if(loggedInUser.Role=="Doctor"){
            return RedirectToAction("IndexDoctor");
        }
        else{
            return RedirectToAction("Index");
        }}
    catch (UnauthorizedAccessException ex)
    {
        ViewBag.ErrorMessage = ex.Message;
        Console.WriteLine("ex.Message", ex.Message);
        TempData["ErrorMessage"] = "Access Denied: You cannot delete this profile.";
        ViewBag.DoctorId = id; 
        return View(model);
    }
}
public IActionResult Profile(Guid id) 
{
    var doctor = DataSeed.Doctors.FirstOrDefault(d => d.DoctorInfo.Id == id);

    if (doctor == null)
    {
        return NotFound();
    }

    return View(doctor);
}
    public IActionResult Index()
    {
        DataSeed.Initialize();
        Console.WriteLine("DataSeed.Doctors", DataSeed.Doctors);
      
           return View(DataSeed.Doctors);
        
       
    }
    public IActionResult IndexDoctor()
    {      
           return View();   
       
    }
}