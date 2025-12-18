using ClinicAppointment_System.Data;
using ClinicAppointment_System.Models.Entittes;

namespace ClinicAppointment_System.Models.Schedule;
using ClinicAppointment_System.Data;

public class ScheduleService: IScheduleService
{
    public ScheduleService()
    {
    }
    
    public void UpdateAppointment(DoctorSchedule app)
    {
        var oldApp = GetAppointmentSlot(app.Id);
        oldApp.StartTime = app.StartTime;
        oldApp.EndTime =  app.EndTime;
        
        // update sched in doctor appointments
        var oldAppDoce= DataSeed.Doctors.First(a => a.DoctorInfo.Id == app.DoctorId).DoctorSchedule.First(a=>a.Id==oldApp.Id);
        oldAppDoce.StartTime = app.StartTime;
        oldAppDoce.EndTime =  app.EndTime;
        // update sched in patient appointments
        if (app.PatientId != null || app.PatientId != Guid.Empty)
        {
            var schedIndexPat= DataSeed.Patients.First(a => a.Id == app.PatientId).Appointments.First(a=>a.Id==oldApp.Id);;
            schedIndexPat.StartTime = app.StartTime;
            schedIndexPat.EndTime =  app.EndTime;        }
        
    }

    public void DeleteAppointment(DoctorSchedule app)
    {
        var appointmentToRemove = DataSeed.Schedules.FirstOrDefault(x => x.Id == app.Id);
    
        if (appointmentToRemove != null)
        {
            DataSeed.Schedules.Remove(appointmentToRemove);

            var doctor = DataSeed.Doctors.FirstOrDefault(d => d.DoctorSchedule.Any(s => s.Id == app.Id));
            if (doctor != null)
            {
                var docApp = doctor.DoctorSchedule.FirstOrDefault(s => s.Id == app.Id);
                if (docApp != null)
                {
                    doctor.DoctorSchedule.Remove(docApp);
                }
            }

            if (app.PatientId != null && app.PatientId != Guid.Empty)
            {
                var patient = DataSeed.Patients.FirstOrDefault(p => p.Appointments.Any(s => s.Id == app.Id));
                if (patient != null)
                {
                    var patApp = patient.Appointments.FirstOrDefault(s => s.Id == app.Id);
                    if (patApp != null)
                    {
                        patient.Appointments.Remove(patApp);
                    }
                }
            }
        }
    }

    public bool BlockAppointmentSlot(Guid appId)
    {
        var app = GetAppointmentSlot(appId);
        if (!app.IsAavailable) return false;
        app.IsBlocked = true;
        UpdateAppointment(app);
        return true;

    }

    public void UnblockAppointmentSlot(Guid appId)
    {
        var app = GetAppointmentSlot(appId);
        if(!app.IsBlocked) return; 
        app.IsBlocked = false;
        UpdateAppointment(app);
    }
    

    public void ReScheduleAppointmentSlot(Guid appId, DateTime start, DateTime end)
    {
        var app = GetAppointmentSlot(appId);
        app.StartTime  = start;
        app.EndTime = end;
        UpdateAppointment(app);
    }

    public void UpdateState(Guid appId, AppointmentSate state)
    {
        var app = GetAppointmentSlot(appId);
        app.AppointmentSate = state;
        UpdateAppointment(app);
    }

    public DoctorSchedule GetAppointmentSlot(Guid appId)
    {
        return DataSeed.Schedules.First(a => a.Id == appId);
    }

    public List<Doctor> GetAllAppointment()
    {
        return DataSeed.Doctors;
    }

    public Doctor GetAppointmentsForDoctor(Guid doctorId)
    {
        return DataSeed.Doctors.First(a => a.Id == doctorId);
    }

    public void AddAppointment(DoctorSchedule app)
    {
        DataSeed.Schedules.Add(app);
        DataSeed.Doctors.First(d=>d.DoctorInfo.Id == app.DoctorId).DoctorSchedule.Add(app);
        
    }
}