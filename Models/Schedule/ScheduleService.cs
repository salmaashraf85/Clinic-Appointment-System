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
        for(var i = 0 ;i<DataSeed.Schedules.Count;i++)
        {
            if (DataSeed.Schedules[i].Id == app.Id)
            {
                DataSeed.Schedules[i] = app;
                break;
            }
        }
    }

    public void DeleteAppointment(DoctorSchedule app)
    {
        DataSeed.Schedules.Remove(app);
    }

    public void BlockAppointmentSlot(Guid appId)
    {
        for(var i = 0 ;i<DataSeed.Schedules.Count;i++)
        {
            if (DataSeed.Schedules[i].Id == appId)
            {
                DataSeed.Schedules[i].IsBlocked = true;
                break;
            }
        }
    }

    public void UnblockAppointmentSlot(Guid appId)
    {
        for(var i = 0 ;i<DataSeed.Schedules.Count;i++)
        {
            if (DataSeed.Schedules[i].Id == appId)
            {
                DataSeed.Schedules[i].IsBlocked = true;
                break;
            }
        }
    }

    public void ReScheduleAppointmentSlot(Guid appId, TimeSpan start, TimeSpan end)
    {
        for(var i = 0 ;i<DataSeed.Schedules.Count;i++)
        {
            if (DataSeed.Schedules[i].Id == appId)
            {
                DataSeed.Schedules[i].StartTime = start;
                DataSeed.Schedules[i].EndTime = end;
                break;
            }
        }
    }

    public void UpdateState(Guid appId, AppointmentSate state)
    {
        
        for(var i = 0 ;i<DataSeed.Schedules.Count;i++)
        {
            if (DataSeed.Schedules[i].Id == appId)
            {
                DataSeed.Schedules[i].AppointmentSate = state;
                break;
            }
        }
    }
}