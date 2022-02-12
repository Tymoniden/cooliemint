namespace CoolieMint.WebApp.Services.Automation
{
    public interface IAutomationEntryQueueService
    {
        AutomationEntry PopAutomationEntry();
        void PushAutomationEntry(AutomationEntry automationEntry);
    }
}