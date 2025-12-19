using ClinicAppointment_System.Data;
using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models;

public class AppointmentScheduler :IAppointmentScheduler
{
    private static AppointmentScheduler _instance;
    private static readonly object _lock = new();
    private readonly List<DoctorSchedule> _appointments;
    
    private AppointmentScheduler()
    {
        _appointments = new List<DoctorSchedule>();
    }

    public static AppointmentScheduler Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                { 
                    _instance ??=new AppointmentScheduler(); 
                }
            }

            return _instance;
        }
    }

    public bool IsAvailable(Guid doctorId, DateTime startTime, DateTime endTime)
    {
        return !_appointments.Any(a =>
            a.DoctorId == doctorId &&
            a.StartTime==startTime &&
            a.EndTime==endTime
        );
    }

    public void Book(DoctorSchedule appointment)
    {
        lock (_lock)
        {
            if (!IsAvailable(
                    appointment.DoctorId,
                    appointment.StartTime,
                    appointment.EndTime))
            {
                throw new Exception("Slot already booked");
            }

            _appointments.Add(appointment);
        }
    }
    
    
    // 🔑 Load from DataSeed
    public void LoadFromSeed()
    {
        if (_appointments.Any()) return;

        _appointments.AddRange(DataSeed.Schedules);
    }

    public List<DoctorSchedule> GetAll()
    {
        return _appointments;
    }
}
    
    
