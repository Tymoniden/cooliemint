using System;

namespace WebControlCenter.Models
{
    public interface IControlModel
    {
        Guid Id { get; set; }
        string Identifier { get; }
        string Title { get; }
        string Adapter { get; }
        ControlAlignment Alignment { get; }
        ControlSize Size { get; }
    }
}