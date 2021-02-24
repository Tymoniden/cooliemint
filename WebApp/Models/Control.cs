using System;

namespace WebControlCenter.Models
{
    public class Control
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StateId { get; set; }
    }
}
