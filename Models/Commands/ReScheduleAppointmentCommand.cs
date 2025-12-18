using System.Runtime.InteropServices.JavaScript;
using ClinicAppointment_System.Models.Schedule;

namespace ClinicAppointment_System.Models.Commands;

public class ReScheduleAppointmentCommand(
    IScheduleService service,
    Guid appId,
    DateTime start,
    DateTime end) : ICommand
{

    public bool Execute()
    {
        try
        {
            service.ReScheduleAppointmentSlot(appId, start, end);
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