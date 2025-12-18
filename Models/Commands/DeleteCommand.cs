using ClinicAppointment_System.Models.Entittes;
using ClinicAppointment_System.Models.Schedule;

namespace ClinicAppointment_System.Models.Commands;

public class DeleteCommand (IScheduleService service, Guid appId): ICommand
{
    private DoctorSchedule? deletedSchedule=null;
    public bool Execute()
    {
        try
        {
            service.DeleteAppointment(service.GetAppointmentSlot(appId));
            return true;
            
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool Redo()
    {
        if (deletedSchedule != null)
        {
            service.AddAppointment(deletedSchedule);
            deletedSchedule = null;
        }

        return true;
    }
}