namespace WebControlCenter.Models
{
    public interface IControlModel
    {
        string Identifier { get; }
        string Title { get; }
        string Adapter { get; }
        ControlAlignment Alignment { get; }
        ControlSize Size { get; }
    }
}