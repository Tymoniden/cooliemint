using System;
using System.Collections.Generic;
using WebControlCenter.CommandAdapter.ShowCase;
using WebControlCenter.Repository;

namespace WebControlCenter.Models
{
    public class ShowCaseControlModel : IControlModel
    {
        public ShowCaseControlModel()
        {
        }

        public ShowCaseControlModel(ShowCaseInitializationArgument arguments)
        {
            Identifier = arguments.Identifier;
            Rows = arguments.Rows;
            Columns = arguments.Columns;
            LightStartIndex = arguments.LightStartIndex;
        }

        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Adapter { get; set; } = "Mqtt:ShowCaseAdapter";
        public ControlAlignment Alignment { get; set; }
        public ControlSize Size { get; set; } = ControlSize.Full;
        public int LightStartIndex { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}