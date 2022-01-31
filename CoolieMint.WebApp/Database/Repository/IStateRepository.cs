using WebControlCenter.Database.Entities;

namespace WebControlCenter.Database.Repository
{
    public interface IStateRepository
    {
        ControllerStateInformation GetStateInformation(string controllerType, string controllerIdentifier, string stateName);
    }
}