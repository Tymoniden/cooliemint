namespace WebControlCenter.CommandAdapter
{
    public interface IControllerState
    {
        string State { get; set; }
        double PowerConsumption { get; set; }
    }
}
