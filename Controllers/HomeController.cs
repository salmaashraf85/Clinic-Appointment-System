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
    public IActionResult Login(string email, string password)
    {
        var doctor = DataSeed.Doctors.FirstOrDefault(d => d.Email == email && d.Password == password);
        if (doctor != null)
        {
            DataSeed.SetCurrentUser(doctor);
            return RedirectToAction("Index");
        }

        var patient = DataSeed.Patients.FirstOrDefault(p => p.Email == email && p.Password == password);
        if (patient != null)
        {
            DataSeed.SetCurrentUser(patient);
            return RedirectToAction("Index");
        }

        ViewBag.Error = "Invalid Email or Password";
        return View();
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User model, string confirmPassword, Roles selectedRole, string specialization)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Password != confirmPassword)
        {
            ViewBag.Error = "Passwords do not match.";
            return View(model);
        }

        try
        {
            // ============================================================
            // هنا نستخدم الفاكتوري كما هو مطلوب في الـ Architecture
            // ============================================================

            // 1. تجهيز الفاكتوري
            UserFactory factorySelector = new UserFactory();

            // 2. الحصول على الاستراتيجية المناسبة (DoctorUser أو PatientUser)
            // تأكد أن اسم الدالة في UserFactory هو CreateUser كما في كودك
            IUser factoryWrapper = factorySelector.CreateUser(selectedRole);

            // 3. إنشاء الكائن (Create the Entity)
            User newUser = factoryWrapper.RegisterUser(model);

            // 4. الحفظ في الداتا سيد (Casting & Saving)
            if (newUser is Doctor doctorEntity)
            {
                // إضافة التخصص إذا كان دكتور
                if (!string.IsNullOrEmpty(specialization))
                {
                    doctorEntity.Specialties.Add(specialization);
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
