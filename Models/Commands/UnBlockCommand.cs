using ClinicAppointment_System.Models.Schedule;

namespace ClinicAppointment_System.Models.Commands;

public class UnBlockCommand (IScheduleService service, Guid appId): ICommand
{
    
    public bool Execute()
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
    public bool Redo()
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
}