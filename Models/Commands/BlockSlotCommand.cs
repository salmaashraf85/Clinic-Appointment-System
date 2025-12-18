using ClinicAppointment_System.Models.Schedule;

namespace ClinicAppointment_System.Models.Commands;

public class BlockSlotCommand (IScheduleService service, Guid appId): ICommand
{
    private IScheduleService _service=service;
    private Guid _appId = appId;

    public bool Execute()
    {
        try
        {
            _service.BlockAppointmentSlot(_appId);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Redo()
    {
        try
        {
            _service.UnblockAppointmentSlot(_appId);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}