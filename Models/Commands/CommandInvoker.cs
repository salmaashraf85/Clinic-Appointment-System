namespace ClinicAppointment_System.Models.Commands;

public class CommandInvoker
{
    private List<ICommand>  _commands = new();

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void ExecuteAllCommands()
    {
        foreach (var command in _commands)
        {
            var result = command.Execute();
            if (!result)
            {
                Console.WriteLine($"Command {command} Faild");
            }
        }
    }
}