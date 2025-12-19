using ClinicAppointment_System.Models;
using ClinicAppointment_System.Models.Entittes;
using ClinicAppointment_System.Models.Payment;

namespace ClinicAppointment_System.Data;
public abstract class DataSeed
{
    
        public static List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public static List<Patient> Patients { get; set; } = new List<Patient>();
        public static List<User> Admins { get; set; } = new List<User>();
        public static List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public static List<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
        private static User? _currentUser;
        private static Roles  _currentUserRole;

        public static User? GetCurrentUser()
        {
            return _currentUser;
        }

        public static Roles GetCurrentUserRole()
        {
            return _currentUserRole;
        }

        public static void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static void Initialize()
        {
            if (Doctors.Any() || Patients.Any()) return;
            var doctor1 = new Doctor
            {
                Id = Guid.NewGuid(),
                Price = 100,
                DoctorInfo=new User{
                    Id = Guid.NewGuid(),
                    FirstName = "Gregory",
                    LastName = "House",
                    Email = "gregory@clinic.com",
                    Password = "123",
                    Role = "Doctor",
                    CreatedAt = DateTime.Now
                },
                DoctorSchedule= new List<DoctorSchedule>(),
                DoctorSpecialist = new DoctorSpecialist
                {
                    Specialization = "Psychiatry",
                    Department = "Psychiatry",
                    YearsOfExperience = 10,
                },
            };
            var doctor2 = new Doctor
            {
                Id = Guid.NewGuid(),
                Price = 50,
                DoctorInfo=new User{
                    Id = Guid.NewGuid(),
                    FirstName = "Ali",
                    LastName = "HAliouse",
                    Email = "Ali@clinic.com",
                    Password = "123",
                    Role = "Doctor",
                    CreatedAt = DateTime.Now
                },
                DoctorSchedule= new List<DoctorSchedule>(),
                DoctorSpecialist = new DoctorSpecialist
                {
                    Specialization = "Psychiatry",
                    Department = "Psychiatry",
                    YearsOfExperience = 10,
                },
            };

            var patient1 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john@gmail.com",
                Password = "123",
                Appointments = new List<DoctorSchedule>(), 
                CreatedAt = DateTime.Now,
                Wallet = 500
            };

            var patient2 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Sarah",
                LastName = "Connor",
                Email = "sarah@gmail.com",
                Password = "123",
                Appointments = new List<DoctorSchedule>(),
                CreatedAt = DateTime.Now,
                Wallet = 1000
                
            };
            var admin = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ahmed",
                LastName = "Ali",
                Email = "admin@gmail.com",
                Password = "123",
                CreatedAt = DateTime.Now,
                Role = "Admin"
            };

            // --- Seed Appointments ---
            // Creating slots for Doctor 1 (Gregory House)
            var app1 = new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor1.Id,
                StartTime = new DateTime(2025, 12, 19, 14, 30, 0),
                EndTime = new  DateTime(2025, 12, 19, 15, 30, 0),
            };

            var app2 = new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor1.Id,
                StartTime = new DateTime(2025, 12, 25, 14, 30, 0), 
                EndTime = new  DateTime(2025, 12, 25, 15,30,0), 
            };

            var app3 = new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor2.Id,
                StartTime = new DateTime(2025, 12, 20, 14, 30, 0),
                EndTime = new  DateTime(2025, 12, 20, 15, 30, 0), 
            };

            // --- Linking Data ---
            patient1.Appointments.Add(app1);
            app1.IsAavailable = false;
            app1.AppointmentSate = AppointmentSate.Pending;
            app1.PatientId=patient1.Id;
            Schedules.Add(app1);
            Schedules.Add(app2);
            Schedules.Add(app3);

            doctor1.DoctorSchedule.Add(app1);
            doctor1.DoctorSchedule.Add(app2);
            doctor2.DoctorSchedule.Add(app3);

            Doctors.Add(doctor1);
            Doctors.Add(doctor2);
            
            Patients.Add(patient1);
            Patients.Add(patient2);
            Admins.Add(admin);
        }
      
}