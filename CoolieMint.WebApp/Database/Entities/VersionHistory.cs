using System;
using System.ComponentModel.DataAnnotations;

namespace WebControlCenter.Database.Entities
{
    public class VersionHistory
    {
        [Key]
        public long Id { get; set; }
        public string Version { get; set; }
        public DateTime InstallTime { get; set; }
    }
}