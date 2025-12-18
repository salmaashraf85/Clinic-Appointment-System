using ClinicAppointment_System.Models.Entittes;
using ClinicAppointment_System.Models.Schedule;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment_System.Models.Commands;

public class ChangeAppointmentStateCommand(Guid appId,
                                           AppointmentSate state,
                                           IScheduleService scheduleService) : ICommand
{
    private  Guid _appId = appId;
    private  AppointmentSate _appointmentSate = state;
    private IScheduleService _scheduleService=scheduleService;
    private AppointmentSate? _oldAppointmentSate = null;

    public bool Execute()
    {
        try
        {
            _oldAppointmentSate = _appointmentSate;
            _scheduleService.UpdateState(_appId, _appointmentSate);
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
                
                _scheduleService.UpdateState(_appId, (AppointmentSate)this._oldAppointmentSate);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}