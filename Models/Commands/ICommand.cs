namespace ClinicAppointment_System.Models.Commands;

public interface ICommand
{
    public bool Execute();
    public bool Redo();
}