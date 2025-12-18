using ClinicAppointment_System.Models.Schedule;

namespace ClinicAppointment_System.Models.Commands;

public class BlockSlotCommand (IScheduleService service, Guid appId): ICommand
{

    public bool Execute()
    {
        try
        {
            return service.BlockAppointmentSlot(appId);
            
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
            service.UnblockAppointmentSlot(appId);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}