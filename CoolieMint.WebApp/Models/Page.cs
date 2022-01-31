using System;
using System.Collections.Generic;

namespace WebControlCenter.Models
{
    public class Page
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public Guid StateId { get; set; }
        public List<Control> Controls { get; set; }
    }
}
