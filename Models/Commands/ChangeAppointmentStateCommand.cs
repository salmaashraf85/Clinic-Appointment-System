using ClinicAppointment_System.Models.Entittes;
using ClinicAppointment_System.Models.Schedule;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment_System.Models.Commands;

public class ChangeAppointmentStateCommand(Guid appId,
                                           AppointmentSate state,
                                           IScheduleService scheduleService) : ICommand
{
    
    private AppointmentSate? _oldAppointmentSate = null;

    public bool Execute()
    {
        try
        {
            _oldAppointmentSate = state;
            scheduleService.UpdateState(appId, state);
            return true;
        }
        catch
        {
            return false;
        }
        
    }

    public bool Redo()
    {
        try
        {
            if (this._oldAppointmentSate != null)
            {
                
                scheduleService.UpdateState(appId, (AppointmentSate)this._oldAppointmentSate);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}