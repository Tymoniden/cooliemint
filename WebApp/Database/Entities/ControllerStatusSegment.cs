using System;
using System.ComponentModel.DataAnnotations;

namespace WebControlCenter.Database.Entities
{
    public class ControllerStatusSegment
    {
        [Key]
        public long Id { get; set; }
        public string ControllerType { get; set; }
        public string ControllerIdentifier { get; set; }
        public DateTime? FirstOnline { get; set; }
        public DateTime? LastOnline { get; set; }
    }
}