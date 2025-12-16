using ClinicAppointment_System.Models;
using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Data;
public abstract class DataSeed
{
    
        public static List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public static List<Patient> Patients { get; set; } = new List<Patient>();
        public static List<Appointment> Appointments { get; set; } = new List<Appointment>();
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
                Appointments = new List<Appointment>(),
            };
            var doctor2 = new Doctor
             {
                Id = Guid.NewGuid(),
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
                Appointments = new List<Appointment>(),
            };

            var patient1 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john@gmail.com",
                Password = "123",
                Appointments = new List<Appointment>(), 
                CreatedAt = DateTime.Now
            };

            var patient2 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Sarah",
                LastName = "Connor",
                Email = "sarah@gmail.com",
                Password = "123",
                Appointments = new List<Appointment>(),
                CreatedAt = DateTime.Now
            };

            // --- Seed Appointments ---
            // Creating slots for Doctor 1 (Gregory House)
            var app1 = new Appointment
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor1.Id,
                Doctor = doctor1,
                StartTime = DateTime.Now.AddDays(1).Date.AddHours(9), 
                EndTime = DateTime.Now.AddDays(1).Date.AddHours(10),  
                IsBooked = false
            };

            var app2 = new Appointment
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor1.Id,
                Doctor = doctor1,
                StartTime = DateTime.Now.AddDays(1).Date.AddHours(10), 
                EndTime = DateTime.Now.AddDays(1).Date.AddHours(11),
                IsBooked = true // Simulating a booked slot
            };

            var app3 = new Appointment
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor2.Id,
                Doctor = doctor2,
                StartTime = DateTime.Now.AddDays(2).Date.AddHours(14), 
                EndTime = DateTime.Now.AddDays(2).Date.AddHours(15),
                IsBooked = false
            };

            // --- Linking Data ---
            
            Appointments.Add(app1);
            Appointments.Add(app2);
            Appointments.Add(app3);

            doctor1.Appointments.Add(app1);
            doctor1.Appointments.Add(app2);
            doctor2.Appointments.Add(app3);

            if (app2.IsBooked)
            {
                patient1.Appointments.Add(app2);
            }

            Doctors.Add(doctor1);
            Doctors.Add(doctor2);
            Patients.Add(patient1);
            Patients.Add(patient2);
        }
      
}