using ClinicAppointment_System.Models;
using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Data;
public abstract class DataSeed
{
    
        public static List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public static List<Patient> Patients { get; set; } = new List<Patient>();
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
                CreatedAt = DateTime.Now
            };

            var patient2 = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = "Sarah",
                LastName = "Connor",
                Email = "sarah@gmail.com",
                Password = "123",
                Appointments = new List<DoctorSchedule>(),
                CreatedAt = DateTime.Now
            };

            // --- Seed Appointments ---
            // Creating slots for Doctor 1 (Gregory House)
            var app1 = new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor1.Id,
                StartTime = new TimeSpan(9, 0, 0), 
                EndTime = new  TimeSpan(10, 0, 0), 
            };

            var app2 = new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor1.Id,
                StartTime = new TimeSpan(10, 0, 0), 
                EndTime = new  TimeSpan(11, 0, 0), 
            };

            var app3 = new DoctorSchedule
            {
                Id = Guid.NewGuid(),
                DoctorId = doctor2.Id,
                StartTime = new TimeSpan(11, 0, 0), 
                EndTime = new  TimeSpan(12, 0, 0), 
            };

            // --- Linking Data ---
            
            Schedules.Add(app1);
            Schedules.Add(app2);
            Schedules.Add(app3);

            doctor1.DoctorSchedule.Add(app1);
            doctor1.DoctorSchedule.Add(app2);
            doctor2.DoctorSchedule.Add(app3);

            if (app2.IsAavailable)
            {
                patient1.Appointments.Add(app2);
            }

            Doctors.Add(doctor1);
            Doctors.Add(doctor2);
            Patients.Add(patient1);
            Patients.Add(patient2);
        }
      
}