namespace WebControlCenter.CustomCommand
{
    public interface ICustomCommandService
    {
        bool ExecuteCommand(Command command);
        Command GetCommand(string callingIdentifier);
        void RegisterCommand(Command command);
    }
}