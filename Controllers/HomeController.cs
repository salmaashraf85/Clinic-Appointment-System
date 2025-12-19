using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicAppointment_System.Models;
using ClinicAppointment_System.Data;

namespace ClinicAppointment_System.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var doctor = DataSeed.Doctors.FirstOrDefault(d => d.Email == model.Email && d.Password == model.Password);
        if (doctor != null)
        {
            DataSeed.SetCurrentUser(doctor);
            return RedirectToAction("Index");
        }

        var patient = DataSeed.Patients.FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password);
        if (patient != null)
        {
            DataSeed.SetCurrentUser(patient);
            return RedirectToAction("Index");
        }
        var admin = DataSeed.Admins.FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password);
        if (admin != null)
        {
            DataSeed.SetCurrentUser(admin);
            return RedirectToAction("Index");
        }
        ViewBag.Error = "Invalid Email or Password";
        return View(model);
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            if (!Enum.TryParse(model.Role, out Roles selectedRole))
            {
                ViewBag.Error = "Invalid Role Selected";
                return View(model);
            }

            UserFactory factorySelector = new UserFactory();
            IUser factoryWrapper = factorySelector.CreateUser(selectedRole);

            User initialData = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };

            User newUser = factoryWrapper.RegisterUser(initialData);

            // if (selectedRole == Roles.Admin)
            // {
            //     DataSeed.Admins.Add(newUser);
            // }
            // else 
            if (newUser is Doctor doctorEntity)
            {
                if (!string.IsNullOrEmpty(model.Specialization))
                {
                    doctorEntity.DoctorSpecialist.Specialization=model.Specialization;
                }
                DataSeed.Doctors.Add(doctorEntity);
            }
            else if (newUser is Patient patientEntity)
            {
                DataSeed.Patients.Add(patientEntity);
            }

            DataSeed.SetCurrentUser(newUser);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Error: " + ex.Message;
            return View(model);
        }
    }
    public IActionResult Logout()
    {
        DataSeed.Logout();
        return RedirectToAction("Login");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}