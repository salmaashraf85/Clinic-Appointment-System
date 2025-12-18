using ClinicAppointment_System.Models.Schedule;

namespace ClinicAppointment_System.Models.Commands;

public class ReScheduleAppointmentCommand(
    IScheduleService service,
    Guid appId,
    TimeSpan start,
    TimeSpan end) : ICommand
{
    private  IScheduleService _service = service;
    private  Guid _appId = appId;
    private  TimeSpan _start = start;
    private  TimeSpan _end = end;
    public bool Execute()
    {
        try
        {
            _service.ReScheduleAppointmentSlot(_appId, _start, _end);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Redo()
    {
        throw new NotImplementedException();
    }
}   